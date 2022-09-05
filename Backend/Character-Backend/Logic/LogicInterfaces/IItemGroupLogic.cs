using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface IItemGroupLogic
    {
        Task<IEnumerable<ItemGroup>> GetItemGroups();     
        Task<ItemGroup> GetItemGroup(int id);
        Task<ItemGroup> PutItemGroup(int id, ItemGroup ItemGroup);
        Task<ItemGroup> PostItemGroup(ItemGroup ItemGroup);
        Task<ItemGroup> DeleteItemGroup(ItemGroup ItemGroup);

        ICollection<Item> GenerateItems(ItemGroup ItemGroup, int timeSpent, ICollection<object> buffs);

    }
}
