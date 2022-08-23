using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface ISkillCategoryRepository : IRepository<SkillCategory>
    {
        Task<List<SkillCategory>> GetSkillCategories();
        Task<SkillCategory> GetSkillCategoryById(int id);
        Task<bool> SkillCategoryExists(string name);
    }
}
