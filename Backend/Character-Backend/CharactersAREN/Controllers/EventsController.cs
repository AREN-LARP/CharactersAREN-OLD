using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Exceptions;
using Logic.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace CharactersAREN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventLogic logic;

        public EventsController(IEventLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/Events
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return Ok(await logic.GetEvents());
        }

        // GET: api/Events/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eve = await logic.GetEvent(id);

            if (eve == null)
            {
                return NotFound();
            }

            return eve;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutSkill(int id, Event eve)
        {
            if (id != eve.Id)
            {
                return BadRequest();
            }

            await logic.PutEvent(id, eve);

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize]
        public async Task<ActionResult<Event>> PostEvent(Event eve)
        {
            try
            {
                await logic.PostEvent(eve);

                return CreatedAtAction("GetEvent", new { id = eve.Id }, eve);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var eve = await logic.GetEvent(id);
            if (eve == null)
            {
                return NotFound();
            }

            await logic.DeleteEvent(eve);

            return eve;
        }

    }
}
