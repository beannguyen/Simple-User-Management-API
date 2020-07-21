using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_User_Management_API.Extension;
using Simple_User_Management_API.Interfaces;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.Models.AccountModels;
using Simple_User_Management_API.Models.ErrorModels;
using Simple_User_Management_API.UnitOfWork.Interface;
using static Simple_User_Management_API.Models.ErrorModels.ErrorType;

namespace Simple_User_Management_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ManageController : Controller
    {
        private readonly UserManagementContext _UserManagementContext;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<User> _Repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly JWTConfig _jWTConfig;

        public ManageController(UserManagementContext userManagementContext, IUnitOfWork unitOfWork, IRepository<User> repository, IMapper mapper, IEmailService emailService, JWTConfig jWTConfig)
        {
            _UserManagementContext = userManagementContext;
            _UnitOfWork = unitOfWork;
            _Repository = repository;
            _mapper = mapper;
            _emailService = emailService;
            _jWTConfig = jWTConfig;
        }
        [HttpPost("UpdateProfile")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileModel model)
        {
            var result = await _Repository.GetWhere(x => x.Email == model.Email);
            var user = result.ToList().FirstOrDefault();
            if (user.Email != null && user.Email != string.Empty)
            {
                user = _mapper.Map<UpdateProfileModel, User>(model);
                await _Repository.Update(user);
                _UnitOfWork.CommitAsync();
                return Ok(model);
            }
            else return Ok(new ErrorModel(EmailNotExist.ErrorCode, EmailNotExist.Message));
        }
        [HttpPost("UpdatePassword")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordModel model)
        {
            var result = await _Repository.GetWhere(x => x.Email == model.Email);
            var user = result.ToList().FirstOrDefault();
            if (user.Email != null && user.Email != string.Empty)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    if (model.OldPassword.HashPassword(_jWTConfig.Secret) == user.Password)
                    {
                        user.Password = model.Password;
                        await _Repository.Update(user);
                        _UnitOfWork.CommitAsync();
                        return Ok(model);
                    }
                    else return Ok(new ErrorModel(OldPasswordNotCorrect.ErrorCode, OldPasswordNotCorrect.Message));
                }
                else return Ok(new ErrorModel(PasswordValidationErrorModel.ErrorCode, PasswordValidationErrorModel.Message));
            }
            else return Ok(new ErrorModel(EmailNotExist.ErrorCode, EmailNotExist.Message));
        }
    }
}