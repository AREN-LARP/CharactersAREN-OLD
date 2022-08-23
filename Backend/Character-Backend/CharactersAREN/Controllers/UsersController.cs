using System.Collections.Generic;
using System.Security.Claims;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserLogic logic;

        public UsersController(IUserLogic logic)
        {
            this.logic = logic;
        }


        // GET: api/Users
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await logic.GetUsers());
        }

        [HttpGet, Authorize]
        [Route("/api/[controller]/CurrentUser/{authId}")]
        public async Task<ActionResult<User>> GetCurrentUser(string authId)
        {            
            var user = await logic.GetCurrentUser(authId);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // GET: api/Users/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await logic.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await logic.PutUser(id, user);

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                await logic.PostUser(user);

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (ObjectAlreadyExistsException)
            {
                return new ConflictResult();
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await logic.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await logic.DeleteUser(user);

            return user;
        }

    }
}
