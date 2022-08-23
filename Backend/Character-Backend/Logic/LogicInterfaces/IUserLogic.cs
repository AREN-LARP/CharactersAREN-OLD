using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface IUserLogic
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetCurrentUser(string id);
        Task<User> GetUser(int id);
        Task<User> PutUser(int id, User user);
        Task<User> PostUser(User user);
        Task<User> DeleteUser(User user);
    }
}
