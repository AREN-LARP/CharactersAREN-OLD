using DAL.DALInterfaces;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CharacterRepository : Repository<Character>, ICharacterRepository
    {
        public CharacterRepository(CharacterContext context) : base(context) { }
        public async Task<bool> CharacterExists(string name)
        {
            IEnumerable<Character> characters = await GetCharacters();
            return characters.Any(c => c.IcName == name);
        }

        public async Task<Character> GetCharacterById(int id)
        {
            IEnumerable<Character> characters = await GetCharacters();
            return characters.FirstOrDefault(c => c.Id == id);
        }

        public async Task<List<Character>> GetCharacters()
        {
            return await GetAll().Include(c => c.User).Include(c => c.Skills).Select(c => new Character
            {
                IcName = c.IcName,
                Backstory = c.Backstory,
                Id = c.Id,
                Status = c.Status,
                User = c.User,
                Career = c.Career,
                Skills = c.Skills.Select(s => new Skill { Id = s.Id, Level = s.Level, Name = s.Name, Description = s.Description, SkillCategory = s.SkillCategory}).ToList()
            }).ToListAsync();
        }

        public async Task<List<Character>> GetUserCharacters(int userId)
        {
            IEnumerable<Character> characters = await GetCharacters();
            return characters.Where(c => c.User.Id == userId).ToList();
        }
        public async Task<IEnumerable<Skill>> GetCharacterSkills(int characterId)
        {
            IEnumerable<Character> characters = await GetCharacters();
            return characters.Single(c => c.Id == characterId).Skills;
        }

        public override async Task<Character> PutEntity(Character entity)
        {
            var dbCharacter = _context.Characters.Where(c => c.Id == entity.Id).Include(c => c.Skills).SingleOrDefault();

            if (dbCharacter != null)
            {
                List<Skill> skills = new List<Skill>();
                foreach (var skill in entity.Skills)
                {
                    var skillInDb = _context.Skills.Find(skill.Id);
                    skills.Add(skillInDb);
                }
                dbCharacter.Skills = skills;
                dbCharacter.Backstory = entity.Backstory;
                dbCharacter.IcName = entity.IcName;
                await _context.SaveChangesAsync();
                return dbCharacter;
            }
            throw new ArgumentException($"Character with id {entity.Id} not found");
        }
    }
}
