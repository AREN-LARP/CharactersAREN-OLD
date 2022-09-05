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
            return await GetAll().ToListAsync();
        }

        public async Task<FactionLoot> GetFactionLootById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> FactionLootExists(string name)
        {
            return await GetAll().AnyAsync(s => s.Name == name);
        }
    }
}
