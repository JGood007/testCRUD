using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sampleCRUD.Model
{
    public class UsersMockRepository : IUsers
    {
        List<Users> _usersList = new List<Users>();
      
        public Users CreateUser(Users users)
        {
            users.id = 1;
            if (_usersList.Count() != 0)
                users.id = _usersList.Max(e => e.id) + 1;
            _usersList.Add(users);
            return users;
        }

        public Users DeleteUser(int id)
        {
            Users removeuser = _usersList.Find(e => e.id == id);
            if (removeuser != null) { 
                _usersList.Remove(removeuser);
            }
            return removeuser;
        }

        public IEnumerable<Users> GetUsers()
        {
            return _usersList;
        }

        public Users GetUsers(int id)
        {
            return _usersList.FirstOrDefault(e => e.id == id);
        }

        public Users UpdateUser(int id, Users modifiedUser)
        {
            Users modUser = _usersList.FirstOrDefault(e => e.id == id);
            if (modUser != null)
            {
                modUser.name = modifiedUser.name;
                modUser.email = modifiedUser.email;
                modUser.password = modifiedUser.password;
                modUser.updated_at = modifiedUser.updated_at;
            }
            return modUser;
        }
    }
}
