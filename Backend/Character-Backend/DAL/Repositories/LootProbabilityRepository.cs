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
    public class LootProbabilityRepository : Repository<LootProbability>, ILootProbabilityRepository
    {
        public LootProbabilityRepository(CharacterContext context) : base(context) { }

        public async Task<List<LootProbability>> GetLootProbabilities()
        {
            return await GetAll().Include(lp => lp.ItemGroup).Select(lp => new LootProbability
            {
                Id = lp.Id,
                ItemGroup = lp.ItemGroup,
                Probability = lp.Probability,
                MinAmount = lp.MinAmount,
                MaxAmount = lp.MaxAmount
            }).ToListAsync();
        }

        public async Task<LootProbability> GetLootProbabilityById(int id)
        {
            IEnumerable<LootProbability> lootProbabilities = await GetLootProbabilities();
            return lootProbabilities.FirstOrDefault(lp => lp.Id == id);
        }

        public async Task<bool> LootProbabilityExists(int itemGroupId)
        {
            return await GetAll().AnyAsync(s => s.ItemGroup.Id == itemGroupId);
        }

        public override async Task<LootProbability> PutEntity(LootProbability entity)
        {
            var dbLootProbability = _context.LootProbabilities.Where(i => i.Id == entity.Id).Include(i => i.ItemGroup).SingleOrDefault();

            if (dbLootProbability != null)
            {
                var itemGroupInDB = _context.ItemGroups.Find(entity.ItemGroup.Id);
                dbLootProbability.ItemGroup = itemGroupInDB;
                dbLootProbability.Probability = entity.Probability;
                dbLootProbability.MinAmount = entity.MinAmount;
                dbLootProbability.MaxAmount = entity.MaxAmount;
                await _context.SaveChangesAsync();
                return dbLootProbability;
            }
            throw new ArgumentException($"LootProbability with id {entity.Id} not found");
        }
    }
}
