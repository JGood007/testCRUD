﻿using System;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sampleCRUD.Model;
using sampleCRUD.Securities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace sampleCRUD.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUsers users;
        private readonly IDataProtector dataProtector;

        //UsersMockRepository UsersMock = new UsersMockRepository();

        public EmployeeController(IUsers users, IDataProtectionProvider dataProtectionProvider, DPPurposeStrings dPPurposeStrings)
        {
            this.users = users;
            dataProtector = dataProtectionProvider.CreateProtector(dPPurposeStrings.ClientIDKey);
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<Users>> Get()
        {
            return users.GetUsers().ToList();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<Users> Get(int id)
        {
            return users.GetUsers(id);
        }

        // POST api/users
        //[HttpPost]
        //public ActionResult<Users> Post([FromBody] Users newuser)
        //{
        //    Users adduser = new Users() {
        //        name = newuser.name,
        //        email = newuser.email,
        //        password = dataProtector.Protect(newuser.password),
        //        updated_at =null,
        //   };

        //   return users.CreateUser(adduser);
        //}

        // PUT api/users/5
        [HttpPut("{id}")]
        public ActionResult<Users> Put(int id, UsersUpdate modifyUser)
        {
            //var currentUser = HttpContext.User;
            //var nameid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value);

            //if (nameid == id)
            //{
            Users moduser = users.GetUsers(id);
            moduser.name = modifyUser.name;
            moduser.email = modifyUser.email;

            return users.UpdateUser(id, moduser);
            //}

            //var errorLog = new Error()
            //{
            //    Code = "400",
            //    Message = $"You're not allowed to edit this id({id})!"
            //};

            //return BadRequest(errorLog);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public ActionResult<Users> Delete(int id)
        {
            //var currentUser = HttpContext.User;
            //var nameid = Convert.ToInt32(currentUser.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value);

            //if (nameid == id)
            //{
                return users.DeleteUser(id);
            //}
            //var errorLog = new Error()
            //{
            //    Code = "400",
            //    Message = $"You're not allowed to delete this id({id})!"
            //};

            //return BadRequest(errorLog);
        }
    }
}
