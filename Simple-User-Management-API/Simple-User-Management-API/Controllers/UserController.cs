using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.UnitOfWork.Interface;
using System;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManagementContext _UserManagementContext;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<User> _Repository;

        public UserController(UserManagementContext userManagementContext, IUnitOfWork unitOfWork, IRepository<User> repository)
        {
            _UserManagementContext = userManagementContext;
            _UnitOfWork = unitOfWork;
            _Repository = repository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create([FromBody] User user)
        {
            try
            {
                await _Repository.Add(user);
                _UnitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}