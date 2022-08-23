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
   public class CareerRepository : Repository<Career>, ICareerRepository
    {
        public CareerRepository(CharacterContext context) : base(context) { }

        public async Task<List<Career>> GetCareers()
        {
            return await GetAll().Include(c => c.Skill).Select(c => new Career
            {
                Name = c.Name,
                Id = c.Id,
                Skill = c.Skill
            }).ToListAsync();
        }

        public async Task<Career> GetCareerById(int id)
        {
            IEnumerable<Career> careers = await GetCareers();
            return careers.FirstOrDefault(c => c.Id == id);
        }

        public async Task<bool> CareerExists(string name)
        {
            return await GetAll().AnyAsync(c => c.Name == name);
        }
    }
}
