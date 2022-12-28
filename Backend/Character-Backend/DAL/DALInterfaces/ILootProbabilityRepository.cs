using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface ILootProbabilityRepository : IRepository<LootProbability>
    {
        Task<List<LootProbability>> GetLootProbabilities();
        Task<LootProbability> GetLootProbabilityById(int id);
        Task<bool> LootProbabilityExists(int itemGroupId);
    }
}
