using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.LogicInterfaces
{
    public interface ICharacterLogic
    {
        Task<IEnumerable<Character>> GetCharacters();
        Task<IEnumerable<Character>> GetUserCharacters(int userId);        
        Task<Character> GetCharacter(int id);
        Task<Character> PutCharacter(int id, Character character, bool isAdmin);
        Task<Character> PostCharacter(Character character);
        Task<Character> DeleteCharacter(Character character);


        Task<IEnumerable<Skill>> GetCharacterSkills(int characterId);

    }
}
