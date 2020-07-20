using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple_User_Management_API.Interfaces;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.Models.AccountModels;
using Simple_User_Management_API.Models.AccountViewModels;
using Simple_User_Management_API.Models.ErrorModels;
using Simple_User_Management_API.UnitOfWork.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Simple_User_Management_API.Models.EmailType;
using static Simple_User_Management_API.Models.ErrorModels.ErrorModel;
using static Simple_User_Management_API.Models.ErrorModels.ErrorType;

namespace Simple_User_Management_API.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManagementContext _UserManagementContext;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<User> _Repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountController(UserManagementContext userManagementContext, IUnitOfWork unitOfWork, IRepository<User> repository, IMapper mapper, IEmailService emailService)
        {
            _UserManagementContext = userManagementContext;
            _UnitOfWork = unitOfWork;
            _Repository = repository;
            _mapper = mapper;
            _emailService = emailService;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _Repository.CountWhere(x => x.Email == model.Email && x.Password == model.Password);
            if (result == 1)
            {
                return Ok(model);
            }
            else
            {
                return Ok(new ErrorModel(LoginErrorModel.ErrorCode,LoginErrorModel.Message));
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword) return Ok(new ErrorModel(PasswordValidationErrorModel.ErrorCode,PasswordValidationErrorModel.Message));
            if (await _Repository.CountWhere(x => x.Email == model.Email) == 0)
            {
                var user = _mapper.Map<User>(model);
                await _Repository.Add(user);
                _UnitOfWork.CommitAsync();
                return Ok(model);
            }
            else return Ok(new ErrorModel(AlreadyExistErrorModel.ErrorCode, AlreadyExistErrorModel.Message));
        }

        [HttpPost("ForgotPassword")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            var result = await _Repository.GetWhere(x => x.Email == model.Email);
            var user = result.ToList().FirstOrDefault();
            if(user.Email != null && user.Email != string.Empty)
            {
                _emailService.Send(model.Email, ResetPassword.Subject, $"{ResetPassword.Body} {user.Password}");
                return Ok(model);
            }
            else return Ok(new ErrorModel(EmailNotExist.ErrorCode, EmailNotExist.Message));
        }
    }
}