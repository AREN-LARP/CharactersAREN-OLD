using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface IFactionLootRepository : IRepository<FactionLoot>
    {
        Task<List<FactionLoot>> GetFactionLoots();
        Task<FactionLoot> GetFactionLootById(int id);
        Task<bool> FactionLootExists(string name);
    }
}
