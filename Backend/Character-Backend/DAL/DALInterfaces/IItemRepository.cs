using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<List<Item>> GetItems();
        Task<Item> GetItemById(int id);
        Task<bool> ItemExists(string name);
    }
}
