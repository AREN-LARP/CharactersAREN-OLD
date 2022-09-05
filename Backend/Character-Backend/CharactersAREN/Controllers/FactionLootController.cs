using DAL.Exceptions;
using Logic.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharactersAREN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactionLootController : ControllerBase
    {
        private readonly IFactionLootLogic logic;

        public FactionLootController(IFactionLootLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/FactionLoots
        [HttpGet, Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<IEnumerable<FactionLoot>>> GetFactionLoots()
        {
            return Ok(await logic.GetFactionLoots());
        }

        // GET: api/FactionLoots/5
        [HttpGet("{id}"), Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<FactionLoot>> GetFactionLoot(int id)
        {
            var factionLoot = await logic.GetFactionLoot(id);

            if (factionLoot == null)
            {
                return NotFound();
            }

            return factionLoot;
        }

        // PUT: api/FactionLoots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Policy = "WriteAccess")]
        public async Task<IActionResult> PutFactionLoot(int id, FactionLoot factionLoot)
        {
            if (id != factionLoot.Id)
            {
                return BadRequest();
            }

            await logic.PutFactionLoot(id, factionLoot);

            return Ok(factionLoot);
        }

        // POST: api/FactionLoots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult<FactionLoot>> PostFactionLoot(FactionLoot factionLoot)
        {
            try
            {
                await logic.PostFactionLoot(factionLoot);

                return CreatedAtAction("GetFactionLoot", new { id = factionLoot.Id }, factionLoot);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // POST: api/FactionLoot/{id}/generateLoot
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/generateLoot")]
        public async Task<ActionResult<IEnumerable<Tuple<Item, int>>>> GenerateLoot(int id)
        {
            var factionLoot = await logic.GetFactionLoot(id);
            if (factionLoot == null)
            {
                return NotFound();
            }
            return Ok(logic.GenerateLoot(factionLoot));
        }

        // DELETE: api/FactionLoot/5
        [HttpDelete("{id}"), Authorize(Policy = "DeleteAccess")]
        public async Task<ActionResult<FactionLoot>> DeleteFactionLoot(int id)
        {
            var factionLoot = await logic.GetFactionLoot(id);
            if (factionLoot == null)
            {
                return NotFound();
            }

            await logic.DeleteFactionLoot(factionLoot);

            return factionLoot;
        }

    }
}
