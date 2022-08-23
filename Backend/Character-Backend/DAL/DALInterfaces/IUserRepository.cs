using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<bool> UserExists(string ocName);
        Task<User> GetUserByAuthId(string id);
    }
}
