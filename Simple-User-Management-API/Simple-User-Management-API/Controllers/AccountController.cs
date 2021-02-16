using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple_User_Management_API.Extension;
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
        private readonly JWTConfig _jWTConfig;

        public AccountController(UserManagementContext userManagementContext, IUnitOfWork unitOfWork, IRepository<User> repository, IMapper mapper, IEmailService emailService, JWTConfig jWTConfig)
        {
            _UserManagementContext = userManagementContext;
            _UnitOfWork = unitOfWork;
            _Repository = repository;
            _mapper = mapper;
            _emailService = emailService;
            _jWTConfig = jWTConfig;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _Repository.CountWhere(x => x.Email == model.Email && x.Password == model.Password.HashPassword(_jWTConfig.Secret));
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
                string randompass = CreateRandomPasswordWithRandomLength();
                user.Password = randompass.HashPassword(_jWTConfig.Secret);
                await _Repository.Add(user);
                _UnitOfWork.CommitAsync();
                _emailService.Send(model.Email, ResetPassword.Subject, $"{ResetPassword.Body} {randompass}");
                return Ok(model);
            }
            else return Ok(new ErrorModel(EmailNotExist.ErrorCode, EmailNotExist.Message));
        }
        private static string CreateRandomPasswordWithRandomLength()
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Minimum size 8. Max size is number of all allowed chars.  
            int size = random.Next(8, validChars.Length);

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
    }
}