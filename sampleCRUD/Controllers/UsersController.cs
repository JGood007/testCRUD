using System;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sampleCRUD.Model;
using sampleCRUD.Securities;

namespace sampleCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsers users;
        private readonly IDataProtector dataProtector;

        //UsersMockRepository UsersMock = new UsersMockRepository();

        public UsersController(IUsers users, IDataProtectionProvider dataProtectionProvider, DPPurposeStrings dPPurposeStrings)
        {
            this.users = users;
            dataProtector = dataProtectionProvider.CreateProtector(dPPurposeStrings.ClientIDKey);
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<Users>> Get()
        {
            //Tightly coupled
            //IEnumerable<Users> userslist = UsersMock.GetUsers();
            //return userslist.ToList();
            //Loosely coupled
            return users.GetUsers().ToList();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<Users> Get(int id)
        {            
            return users.GetUsers(id);
        }

        // POST api/users
        [HttpPost]
        public ActionResult<Users> Post([FromBody] Users newuser)
        {
            Users adduser = new Users() {
                name = newuser.name,
                email = newuser.email,
                password = dataProtector.Protect(newuser.password),
                updated_at =null,
           };
             
           return users.CreateUser(adduser);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public ActionResult<Users> Put(int id, [FromBody] Users modifyUser)
        {
            Users moduser = new Users()
            {
                name = modifyUser.name,
                email = modifyUser.email,
                password = dataProtector.Protect(modifyUser.password),
            };

            return users.UpdateUser(id, moduser);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public ActionResult<Users> Delete(int id)
        {
           return users.DeleteUser(id);
        }
    }
}
