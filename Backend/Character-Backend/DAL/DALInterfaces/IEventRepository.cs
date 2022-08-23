using System;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<List<Event>> GetEvents();
        Task<Event> GetEventById(int id);
        Task<bool> EventExists(string name);
    }
}