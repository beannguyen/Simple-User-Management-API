using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_User_Management_API.Models;

namespace Simple_User_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateController : ControllerBase
    {
        private UserManagementContext _UserManagementContext;

        public CreateController(UserManagementContext userManagementContext)
        {
            _UserManagementContext = userManagementContext;
        }
        [HttpPost("Role")]
        public async Task<ActionResult<string>> CreateRole([FromBody] Role role) 
        {
            try
            {
                await _UserManagementContext.Roles.AddAsync(role);
                await _UserManagementContext.SaveChangesAsync();
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