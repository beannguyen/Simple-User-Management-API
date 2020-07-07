using Microsoft.AspNetCore.Mvc;
using Simple_User_Management_API.Models;
using Simple_User_Management_API.UnitOfWork.Interface;
using System;
using System.Threading.Tasks;

namespace Simple_User_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateController : ControllerBase
    {
        private readonly UserManagementContext _UserManagementContext;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IRepository<Role> _Repository;

        public CreateController(UserManagementContext userManagementContext, IUnitOfWork unitOfWork, IRepository<Role> repository)
        {
            _UserManagementContext = userManagementContext;
            _UnitOfWork = unitOfWork;
            _Repository = repository;
        }

        [HttpPost("Role")]
        public async Task<ActionResult<string>> CreateRole([FromBody] Role role)
        {
            try
            {
                await _Repository.Add(role);
                _UnitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex);
                throw;
            }
            return Ok();
        }

        [HttpPost("User")]
        public async Task<ActionResult<string>> CreateUser([FromBody] User user)
        {
            try
            {
                await _UserManagementContext.Users.AddAsync(user);
                await _UserManagementContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Ok(ex);
                throw;
            }
            return Ok();
        }
    }
}