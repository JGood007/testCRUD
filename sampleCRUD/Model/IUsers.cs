using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleCRUD.Model
{
    public interface IUsers
    {
        Login LoginUser(Login users);
        Users CreateUser(Users users);
        IEnumerable<Users> GetUsers();
        Users GetUsers(int id);
        Users UpdateUser(int id, Users modifiedUser);
        Users DeleteUser(int id);
    }
}
