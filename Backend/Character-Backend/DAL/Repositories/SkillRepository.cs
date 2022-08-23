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
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(CharacterContext context) : base(context) { }

        public async Task<List<Skill>> GetSkills()
        {
            return await GetAll().Include(s => s.SkillCategory).ToListAsync();
        }

        public async Task<Skill> GetSkillById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> SkillExists(string name)
        {
            return await GetAll().AnyAsync(s => s.Name == name);
        }
    }
}
