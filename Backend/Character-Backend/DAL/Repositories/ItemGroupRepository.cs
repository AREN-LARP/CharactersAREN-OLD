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
            return await GetAll().Include(ig => ig.Skill).Include(ig => ig.Items).Select(ig => new ItemGroup
            {
                Id = ig.Id,
                Name = ig.Name,
                Skill = ig.Skill,
                Speed = ig.Speed,
                Items = ig.Items.Select(i => new Item { Id = i.Id, Name = i.Name, Description = i.Description, Weight = i.Weight}).ToList()
            }).ToListAsync();
        }

        public async Task<ItemGroup> GetItemGroupById(int id)
        {
            IEnumerable<ItemGroup> itemGroups = await GetItemGroups();
            return itemGroups.FirstOrDefault(ig => ig.Id == id);
        }

        public async Task<IEnumerable<ItemGroup>> GetItemGroupsBySkillId(int id)
        {
            IEnumerable<ItemGroup> itemGroups = await GetItemGroups();
            return itemGroups.Where(ig => ig.Skill.Id == id);
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
