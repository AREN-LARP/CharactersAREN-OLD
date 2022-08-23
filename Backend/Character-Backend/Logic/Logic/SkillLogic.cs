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
    public class SkillLogic : ISkillLogic
    {
        private readonly ISkillRepository _repo;

        public SkillLogic(ISkillRepository repo)
        {
            _repo = repo;
        }

        public async Task<Skill> DeleteSkill(Skill skill)
        {
            return await _repo.DeleteEntity(skill);
        }

        public async Task<Skill> GetSkill(int id)
        {
            var skill = await _repo.GetSkillById(id);

            return skill;
        }

        public async Task<IEnumerable<Skill>> GetSkills()
        {
            return await _repo.GetSkills();
        }

        public async Task<Skill> PostSkill(Skill skill)
        {
            var exExist = await _repo.SkillExists(skill.Name);
            if (exExist)
            {
                throw new ObjectAlreadyExistsException(skill.GetType().Name, skill.Name);
            }
            else
            {
                return await _repo.AddEntity(skill);
            }
        }

        public async Task<Skill> PutSkill(int id, Skill skill)
        {
            if (skill.Id != id)
            {
                throw new ArgumentException($"{nameof(PutSkill)}: id in url is not equal to skill id.");
            }

            return await _repo.PutEntity(skill);
        }
    }
}
