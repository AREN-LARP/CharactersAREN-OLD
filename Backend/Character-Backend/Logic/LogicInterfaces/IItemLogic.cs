using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface IItemLogic
    {
        Task<IEnumerable<Item>> GetItems();
        Task<Item> GetItem(int id);
        Task<Item> PutItem(int id, Item item);
        Task<Item> PostItem(Item item);
        Task<Item> DeleteItem(Item item);
    }
}
