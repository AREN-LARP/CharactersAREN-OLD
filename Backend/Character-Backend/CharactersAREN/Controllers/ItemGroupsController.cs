﻿using DAL.Exceptions;
using Logic.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CharactersAREN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemGroupsController : ControllerBase
    {
        private readonly IItemGroupLogic logic;

        public ItemGroupsController(IItemGroupLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/ItemGroups
        [HttpGet, Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<IEnumerable<ItemGroup>>> GetItemGroups()
        {
            return Ok(await logic.GetItemGroups());
        }

        // GET: api/ItemGroups/5
        [HttpGet("{id}"), Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<ItemGroup>> GetItemGroup(int id)
        {
            var itemGroup = await logic.GetItemGroup(id);

            if (itemGroup == null)
            {
                return NotFound();
            }

            return itemGroup;
        }

        // PUT: api/ItemGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Policy = "WriteAccess")]
        public async Task<IActionResult> PutItemGroup(int id, ItemGroup itemGroup)
        {
            if (id != itemGroup.Id)
            {
                return BadRequest();
            }

            await logic.PutItemGroup(id, itemGroup);

            return Ok(itemGroup);
        }

        // POST: api/ItemGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult<ItemGroup>> PostItemGroup(ItemGroup itemGroup)
        {
            try
            {
                await logic.PostItemGroup(itemGroup);

                return CreatedAtAction("GetItemGroup", new { id = itemGroup.Id }, itemGroup);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // POST: api/ItemGroups/{id}/generateItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/generateItems")]
        public async Task<ActionResult<ICollection<Item>>> GenerateItemsFromGroup(int id, int timeSpent)
        {
            var itemGroup = await logic.GetItemGroup(id);
            if (itemGroup == null)
            {
                return NotFound();
            }
            return Ok(logic.GenerateItems(itemGroup, timeSpent, new List<object>()));
        }

        // DELETE: api/ItemGroups/5
        [HttpDelete("{id}"), Authorize(Policy = "DeleteAccess")]
        public async Task<ActionResult<ItemGroup>> DeleteItemGroup(int id)
        {
            var itemGroup = await logic.GetItemGroup(id);
            if (itemGroup == null)
            {
                return NotFound();
            }

            await logic.DeleteItemGroup(itemGroup);

            return itemGroup;
        }

    }
}