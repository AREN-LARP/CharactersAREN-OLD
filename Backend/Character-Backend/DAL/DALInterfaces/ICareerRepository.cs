using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface ICareerRepository : IRepository<Career>
    {
        Task<List<Career>> GetCareers();
        Task<Career> GetCareerById(int id);
        Task<bool> CareerExists(string name);
    }
}