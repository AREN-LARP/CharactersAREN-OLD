using DAL.DALInterfaces;
using DAL.Exceptions;
using Logic.LogicInterfaces;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _repo;

        public UserLogic(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> DeleteUser(User user)
        {
            return await _repo.DeleteEntity(user);
        }

        public async Task<User> GetUser(int id)
        {
            var User = await _repo.GetUserById(id);

            return User;
        }

        public async Task<User> GetCurrentUser(string id)
        {
            var User = await _repo.GetUserByAuthId(id);

            return User;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repo.GetUsers();
        }

        public async Task<User> PostUser(User user)
        {
            var exExist = await _repo.UserExists(user.AuthId);
            if (exExist)
            {
                throw new ObjectAlreadyExistsException(user.GetType().Name, user.AuthId);
            }
            else
            {
                return await _repo.AddEntity(user);
            }
        }

        public async Task<User> PutUser(int id, User user)
        {
            if (user.Id != id)
            {
                throw new ArgumentException($"{nameof(PutUser)}: id in url is not equal to User id.");
            }

            return await _repo.PutEntity(user);
        }
    }
}
