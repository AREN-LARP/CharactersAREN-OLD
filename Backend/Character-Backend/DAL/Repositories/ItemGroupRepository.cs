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

        public override async Task<ItemGroup> PutEntity(ItemGroup entity)
        {
            var dbItemGroup = _context.ItemGroups.Where(i => i.Id == entity.Id).Include(i => i.Items).SingleOrDefault();

            if (dbItemGroup != null)
            {
                List<Item> items = new List<Item>();
                foreach (var item in entity.Items)
                {
                    var itemInDB = _context.Items.Find(item.Id);
                    items.Add(itemInDB);
                }
                dbItemGroup.Items = items;
                dbItemGroup.Name = entity.Name;
                dbItemGroup.Skill = entity.Skill;
                dbItemGroup.Speed = entity.Speed;
                await _context.SaveChangesAsync();
                return dbItemGroup;
            }
            throw new ArgumentException($"ItemGroup with id {entity.Id} not found");
        }
    }
}
