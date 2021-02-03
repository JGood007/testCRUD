using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleCRUD.Model
{
    public class UsersSqlRepository : IUsers
    {
        private readonly AppDbContext context;

        public UsersSqlRepository(AppDbContext context)
        {
            this.context = context;
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
    }
}
