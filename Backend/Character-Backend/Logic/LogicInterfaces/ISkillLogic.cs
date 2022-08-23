using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface ISkillLogic
    {
        Task<IEnumerable<Skill>> GetSkills();
        Task<Skill> GetSkill(int id);
        Task<Skill> PutSkill(int id, Skill skill);
        Task<Skill> PostSkill(Skill skill);
        Task<Skill> DeleteSkill(Skill skill);
    }
}
