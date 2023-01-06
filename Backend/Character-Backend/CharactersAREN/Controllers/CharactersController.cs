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
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterLogic logic;

        public CharactersController(ICharacterLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/Characters
        [HttpGet]//, Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
            return Ok(await logic.GetCharacters());
        }

        [HttpGet]//, Authorize(Policy = "ReadAccess")]
        [Route("/api/[controller]/UserCharacters/{userId}")]
        public async Task<ActionResult<IEnumerable<Character>>> GetUserCharacters(int userId)
        {
            return Ok( await logic.GetUserCharacters(userId));
        }

        [HttpGet]//, Authorize(Policy = "ReadAccess")]
        [Route("/api/[controller]/Skills/{id}")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetCharacterSkills(int id)
        {
            return Ok(await logic.GetCharacterSkills(id));
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]//, Authorize(Policy = "ReadAccess")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
            var character = await logic.GetCharacter(id);

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]//, Authorize(Policy = "WriteAccess")]
        public async Task<IActionResult> PutCharacter(int id, Character character)
        {
            bool isAdmin = User.HasClaim("permissions", "administrator");
            if (id != character.Id)
            {
                return BadRequest();
            }

            var value = await logic.PutCharacter(id, character, isAdmin);

            return Ok(value);
        }

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]//, Authorize(Policy = "WriteAccess")]
        public async Task<ActionResult<Character>> PostCharacter(Character character)
        {
            try
            {
                var result = await logic.PostCharacter(character);

                return CreatedAtAction("GetCharacter", new { id = result.Id }, result);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]//, Authorize(Policy = "DeleteAccess")]
        public async Task<ActionResult<Character>> DeleteCharacter(int id)
        {
            var character = await logic.GetCharacter(id);
            if (character == null)
            {
                return NotFound();
            }

            await logic.DeleteCharacter(character);

            return character;
        }
    }
}
