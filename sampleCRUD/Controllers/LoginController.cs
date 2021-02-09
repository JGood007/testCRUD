using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sampleCRUD.Model;
using sampleCRUD.Securities;

namespace sampleCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsers users;
        private readonly IDataProtector dataProtector;
        public LoginController(IUsers users, IDataProtectionProvider dataProtectionProvider, DPPurposeStrings dPPurposeStrings)
        {
            dataProtector = dataProtectionProvider.CreateProtector(dPPurposeStrings.ClientIDKey);
            this.users = users;
        }
        // POST api/login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] Login user)
        {
            var checkuser = users.GetUsers().ToList().Find(e => e.email == user.email && dataProtector.Unprotect(e.password)== user.password);

            if (checkuser!= null)
            {
                user.password = dataProtector.Protect(user.password);
                return Ok(users.LoginUser(user));
                 
            }
            var errorLog = new Error()
            {
                Code = "400",
                Message = "Invalid email or password!"
            };

            return BadRequest(errorLog);
        }
    }
}