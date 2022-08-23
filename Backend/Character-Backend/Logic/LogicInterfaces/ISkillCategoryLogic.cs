using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface ISkillCategoryLogic
    {
        Task<IEnumerable<SkillCategory>> GetSkillCategories();
        Task<SkillCategory> GetSkillCategory(int id);
        Task<SkillCategory> PutSkillCategory(int id, SkillCategory skillCategory);
        Task<SkillCategory> PostSkillCategory(SkillCategory skillCategory);
        Task<SkillCategory> DeleteSkillCategory(SkillCategory skillCategory);
    }
}
