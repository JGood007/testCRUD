using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sampleCRUD.Model;
using sampleCRUD.Securities;

namespace sampleCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUsers users;
        private readonly IDataProtector dataProtector;
        public RegisterController(IUsers users, IDataProtectionProvider dataProtectionProvider, DPPurposeStrings dPPurposeStrings)
        {
            dataProtector = dataProtectionProvider.CreateProtector(dPPurposeStrings.ClientIDKey);
            this.users = users;
        }

        // POST api/register
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromForm] Users newuser)
        {
            if (ModelState.ErrorCount == 0)
            {
                Users adduser = new Users()
                {
                    name = newuser.name,
                    email = newuser.email,
                    password = dataProtector.Protect(newuser.password),
                    updated_at = null,
                };
                return Ok(users.CreateUser(adduser));
            }
            return BadRequest(ModelState);
        }
    }
}