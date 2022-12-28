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
    public class FactionLootRepository : Repository<FactionLoot>, IFactionLootRepository
    {
        public FactionLootRepository(CharacterContext context) : base(context) { }

        public async Task<List<FactionLoot>> GetFactionLoots()
        {
            return await GetAll().Include(fl => fl.LootProbabilities).Select(fl => new FactionLoot
            {
                Id = fl.Id,
                Name = fl.Name,
                Colour = fl.Colour,
                LootProbabilities = fl.LootProbabilities.Select(lp => new LootProbability { Id = lp.Id, ItemGroup = lp.ItemGroup, Probability = lp.Probability, MinAmount = lp.MinAmount, MaxAmount = lp.MaxAmount}).ToList()

            }).ToListAsync();
        }

        public async Task<FactionLoot> GetFactionLootById(int id)
        {
            IEnumerable<FactionLoot> factionLoots = await GetFactionLoots();
            return factionLoots.FirstOrDefault(s => s.Id == id);
        }

        public async Task<bool> FactionLootExists(string name)
        {
            return await GetAll().AnyAsync(s => s.Name == name);
        }

        public override async Task<FactionLoot> PutEntity(FactionLoot entity)
        {
            var dbFactionLoot = _context.FactionLoots.Where(f => f.Id == entity.Id).Include(f => f.LootProbabilities).SingleOrDefault();

            if (dbFactionLoot != null)
            {
                List<LootProbability> lootProbabilities = new List<LootProbability>();
                foreach (var loot in entity.LootProbabilities)
                {
                    var lootInDB = _context.LootProbabilities.Find(loot.Id);
                    lootProbabilities.Add(lootInDB);
                }
                dbFactionLoot.LootProbabilities = lootProbabilities;
                dbFactionLoot.Name = entity.Name;
                dbFactionLoot.Colour = entity.Colour;
                await _context.SaveChangesAsync();
                return dbFactionLoot;
            }
            throw new ArgumentException($"FactionLoot with id {entity.Id} not found");
        }
    }
}
