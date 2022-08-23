using DAL.DALInterfaces;
using DAL.Exceptions;
using Logic.LogicInterfaces;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class EventLogic : IEventLogic
    {
        private readonly IEventRepository _repo;

        public EventLogic(IEventRepository repo)
        {
            _repo = repo;
        }

        public async Task<Event> DeleteEvent(Event eve)
        {
            return await _repo.DeleteEntity(eve);
        }

        public async Task<Event> GetEvent(int id)
        {
            var eve = await _repo.GetEventById(id);

            return eve;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _repo.GetEvents();
        }

        public async Task<Event> PostEvent(Event eve)
        {
            var exExist = await _repo.EventExists(eve.Name);
            if (exExist)
            {
                throw new ObjectAlreadyExistsException(eve.GetType().Name, eve.Name);
            }
            else
            {
                return await _repo.AddEntity(eve);
            }
        }

        public async Task<Event> PutEvent(int id, Event eve)
        {
            if (eve.Id != id)
            {
                throw new ArgumentException($"{nameof(PutEvent)}: id in url is not equal to event id.");
            }

            return await _repo.PutEntity(eve);
        }
    }
}
