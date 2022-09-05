using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface IFactionLootLogic
    {
        Task<IEnumerable<FactionLoot>> GetFactionLoots();     
        Task<FactionLoot> GetFactionLoot(int id);
        Task<FactionLoot> PutFactionLoot(int id, FactionLoot FactionLoot);
        Task<FactionLoot> PostFactionLoot(FactionLoot FactionLoot);
        Task<FactionLoot> DeleteFactionLoot(FactionLoot FactionLoot);

        IEnumerable<Tuple<Item, int>> GenerateLoot(FactionLoot FactionLoot);

    }
}
