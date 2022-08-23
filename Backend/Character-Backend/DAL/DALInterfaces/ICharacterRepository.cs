using Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DALInterfaces
{
    public interface ICharacterRepository :IRepository<Character>
    {
        Task<List<Character>> GetCharacters();
        Task<List<Character>> GetUserCharacters(int userId);
        Task<IEnumerable<Skill>> GetCharacterSkills(int characterId);
        Task<Character> GetCharacterById(int id);
        Task<bool> CharacterExists(string name);
    }
}
