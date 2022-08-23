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
    public class SkillsController : ControllerBase
    {
        private readonly ISkillLogic logic;

        public SkillsController(ISkillLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/Skills
        [HttpGet, Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            return Ok(await logic.GetSkills());
        }

        // GET: api/Skills/5
        [HttpGet("{id}"), Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<Skill>> GetSkill(int id)
        {
            var skill = await logic.GetSkill(id);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        // PUT: api/Skills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize(Policy = "WriteAccess")]
        public async Task<IActionResult> PutSkill(int id, Skill skill)
        {
            if (id != skill.Id)
            {
                return BadRequest();
            }

            await logic.PutSkill(id, skill);

            return Ok(skill);
        }

        // POST: api/Skills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult<Skill>> PostSkill(Skill skill)
        {
            try
            {
                await logic.PostSkill(skill);

                return CreatedAtAction("GetSkill", new { id = skill.Id }, skill);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}"), Authorize(Policy = "DeleteAccess")]
        public async Task<ActionResult<Skill>> DeleteSkill(int id)
        {
            var skill = await logic.GetSkill(id);
            if (skill == null)
            {
                return NotFound();
            }

            await logic.DeleteSkill(skill);

            return skill;
        }

    }
}
