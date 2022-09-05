using DAL.DALInterfaces;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ItemGroupRepository : Repository<ItemGroup>, IItemGroupRepository
    {
        public ItemGroupRepository(CharacterContext context) : base(context) { }

        public async Task<List<ItemGroup>> GetItemGroups()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<ItemGroup> GetItemGroupById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> ItemGroupExists(string name)
        {
            return await GetAll().AnyAsync(s => s.Name == name);
        }
    }
}
