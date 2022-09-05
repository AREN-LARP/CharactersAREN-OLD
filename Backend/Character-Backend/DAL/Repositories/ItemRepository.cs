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
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(CharacterContext context) : base(context) { }

        public async Task<List<Item>> GetItems()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Item> GetItemById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> ItemExists(string name)
        {
            return await GetAll().AnyAsync(s => s.Name == name);
        }
    }
}
