using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Exceptions;
using Logic.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace CharactersAREN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemLogic logic;

        public ItemsController(IItemLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/Items
        [HttpGet]//, Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return Ok(await logic.GetItems());
        }

        // GET: api/Items/5
        [HttpGet("{id}")]//, Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var skill = await logic.GetItem(id);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]//, Authorize(Policy = "WriteAccess")]
        public async Task<IActionResult> PutItem(int id, Item skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            await logic.PutItem(id, skill);

            return Ok(skill);
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]//, Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult<Item>> PostItem(Item skill)
        {
            try
            {
                await logic.PostItem(skill);

                return CreatedAtAction("GetItem", new { id = skill.Id }, skill);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]//, Authorize(Policy = "DeleteAccess")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var skill = await logic.GetItem(id);
            if (skill == null)
            {
                return NotFound();
            }

            await logic.DeleteItem(skill);

            return skill;
        }

    }
}
