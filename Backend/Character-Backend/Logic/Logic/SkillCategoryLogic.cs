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
    public class SkillCategoryLogic : ISkillCategoryLogic
    {
        private readonly ISkillCategoryRepository _repo;

        public SkillCategoryLogic(ISkillCategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<SkillCategory> DeleteSkillCategory(SkillCategory skillCategory)
        {
            return await _repo.DeleteEntity(skillCategory);
        }

        public async Task<IEnumerable<SkillCategory>> GetSkillCategories()
        {
            var categories = await _repo.GetSkillCategories();
            return categories;
        }

        public async Task<SkillCategory> GetSkillCategory(int id)
        {
            var skillCategory = await _repo.GetSkillCategoryById(id);

            return skillCategory; 
        }

        public async Task<SkillCategory> PostSkillCategory(SkillCategory skillCategory)
        {
            var exExist = await _repo.SkillCategoryExists(skillCategory.Name);
            if (exExist)
            {
                throw new ObjectAlreadyExistsException(skillCategory.GetType().Name, skillCategory.Name);
            }
            else
            {
                return await _repo.AddEntity(skillCategory);
            }
        }

        public async Task<SkillCategory> PutSkillCategory(int id, SkillCategory skillCategory)
        {
            if (skillCategory.Id != id)
            {
                throw new ArgumentException($"{nameof(PutSkillCategory)}: id in url is not equal to character id.");
            }

            return await _repo.PutEntity(skillCategory);
        }
    }
}
