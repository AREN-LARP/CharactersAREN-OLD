using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<List<Skill>> GetSkills();
        Task<Skill> GetSkillById(int id);
        Task<bool> SkillExists(string name);
    }
}
