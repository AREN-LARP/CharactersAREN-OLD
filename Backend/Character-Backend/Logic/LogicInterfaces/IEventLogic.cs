using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface IEventLogic
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEvent(int id);
        Task<Event> PutEvent(int id, Event eve);
        Task<Event> PostEvent(Event eve);
        Task<Event> DeleteEvent(Event eve);
    }
}
