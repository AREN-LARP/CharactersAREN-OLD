using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface IItemGroupRepository : IRepository<ItemGroup>
    {
        Task<List<ItemGroup>> GetItemGroups();
        Task<ItemGroup> GetItemGroupById(int id);
        Task<bool> ItemGroupExists(string name);
    }
}
