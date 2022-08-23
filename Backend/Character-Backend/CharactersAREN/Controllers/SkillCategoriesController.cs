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
    public class SkillCategoriesController : ControllerBase
    {
        private readonly ISkillCategoryLogic logic;

        public SkillCategoriesController(ISkillCategoryLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/SkillCategories
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<SkillCategory>>> GetSkillCategories()
        {
            return Ok(await logic.GetSkillCategories());
        }

        // GET: api/SkillCategories/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<SkillCategory>> GetSkillCategory(int id)
        {
            var skillCategory = await logic.GetSkillCategory(id);

            if (skillCategory == null)
            {
                return NotFound();
            }

            return skillCategory;
        }

        // PUT: api/SkillCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutSkillCategory(int id, SkillCategory skillCategory)
        {
            if (id != skillCategory.Id)
            {
                return BadRequest();
            }

            await logic.PutSkillCategory(id, skillCategory);

            return NoContent();
        }

        // POST: api/SkillCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize]
        public async Task<ActionResult<SkillCategory>> PostSkill(SkillCategory skillCategory)
        {
            try
            {
                await logic.PostSkillCategory(skillCategory);

                return CreatedAtAction("GetSkillCategory", new { id = skillCategory.Id }, skillCategory);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // DELETE: api/SkillCategories/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<SkillCategory>> DeleteSkillCategory(int id)
        {
            var skillCategory = await logic.GetSkillCategory(id);
            if (skillCategory == null)
            {
                return NotFound();
            }

            await logic.DeleteSkillCategory(skillCategory);

            return skillCategory;
        }

    }
}
