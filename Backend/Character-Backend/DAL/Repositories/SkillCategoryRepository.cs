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
    public class SkillCategoryRepository : Repository<SkillCategory>, ISkillCategoryRepository
    {
        public SkillCategoryRepository(CharacterContext context) : base(context) { }

        public async Task<List<SkillCategory>> GetSkillCategories()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<SkillCategory> GetSkillCategoryById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> SkillCategoryExists(string name)
        {
            return await GetAll().AnyAsync(s => s.Name == name);
        }

    }
}