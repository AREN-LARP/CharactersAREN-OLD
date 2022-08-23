using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface ICareerLogic
    {
        Task<IEnumerable<Career>> GetCareers();
        Task<Career> GetCareer(int id);
        Task<Career> PutCareer(int id, Career career);
        Task<Career> PostCareer(Career career);
        Task<Career> DeleteCareer(Career career);
    }
}
