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
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(CharacterContext context) : base(context) { }

        public async Task<List<Event>> GetEvents()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> EventExists(string name)
        {
            return await GetAll().AnyAsync(e => e.Name == name);
        }

    }
}