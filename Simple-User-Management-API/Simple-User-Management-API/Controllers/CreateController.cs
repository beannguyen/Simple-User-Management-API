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
        [HttpPost("Role")]
        public async Task<ActionResult<string>> CreateRole([FromBody] Role role) 
        {
            try
            {
                using (var context = new UserManagementContext())
                {
                    await context.Roles.AddAsync(role);
                    await context.SaveChangesAsync();
                }
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
                using (var context = new UserManagementContext())
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                }
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