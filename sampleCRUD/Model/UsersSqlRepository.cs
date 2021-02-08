using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace sampleCRUD.Model
{
    public class UsersSqlRepository : IUsers
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public UsersSqlRepository(AppDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public Users CreateUser(Users users)
        {
            context.Users.Add(users);
            context.SaveChanges();
            return users;
        }

        public Users DeleteUser(int id)
        {
            Users deluser = context.Users.Find(id);
            if (deluser != null)
            {
                context.Users.Remove(deluser);
                context.SaveChanges();
            }
            return deluser;
        }

        public IEnumerable<Users> GetUsers()
        {
            return context.Users;
        }

        public Users GetUsers(int id)
        {
           return context.Users.Find(id);
        }

        public Login LoginUser(Login users)
        {
            var userprofile = context.Users.FirstOrDefault(e => e.email == users.email); 

            users.token = GenerateJSONWebToken(userprofile);

            return users;
        }

        public Users UpdateUser(int id, Users modifiedUser)
        {
            //modifiedUser.id = id;
            Users updateUser = context.Users.Find(id);
            if (updateUser != null)
            {
                updateUser.email = modifiedUser.email;
                updateUser.name = modifiedUser.name;
                updateUser.password = modifiedUser.password;
                updateUser.updated_at = modifiedUser.updated_at;
                context.SaveChanges();
            }
            //var user = context.Users.Attach(modifiedUser);
            //user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return updateUser;
        }

        private string GenerateJSONWebToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, user.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
                new Claim("datelogin", DateTime.Now.ToString())
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
