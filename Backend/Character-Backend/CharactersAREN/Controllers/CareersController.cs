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
    public class CareersController : ControllerBase
    {
        private readonly ICareerLogic logic;

        public CareersController(ICareerLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/Careers
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Career>>> GetCareers()
        {
            return Ok(await logic.GetCareers());
        }

        // GET: api/Careers/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Career>> GetCareer(int id)
        {
            var career = await logic.GetCareer(id);

            if (career == null)
            {
                return NotFound();
            }

            return career;
        }

        // PUT: api/Careers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutCareer(int id, Career career)
        {
            if (id != career.Id)
            {
                return BadRequest();
            }

            await logic.PutCareer(id, career);

            return NoContent();
        }

        // POST: api/Careers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost, Authorize]
        public async Task<ActionResult<Skill>> PostCareer(Career career)
        {
            try
            {
                await logic.PostCareer(career);

                return CreatedAtAction("GetCareer", new { id = career.Id }, career);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // DELETE: api/Careers/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Career>> DeleteCareer(int id)
        {
            var career = await logic.GetCareer(id);
            if (career == null)
            {
                return NotFound();
            }

            await logic.DeleteCareer(career);

            return career;
        }

    }
}
