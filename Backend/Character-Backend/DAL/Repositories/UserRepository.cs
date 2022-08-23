using DAL.DALInterfaces;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CharacterContext context) : base(context) { }

        public async Task<List<User>> GetUsers()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<User> GetUserByAuthId(string id)
        {
            return await GetAll().FirstOrDefaultAsync(u => u.AuthId == id);
        }

        public async Task<bool> UserExists(string authId)
        {
            return await GetAll().AnyAsync(u => u.AuthId == authId);
        }
    }
}
