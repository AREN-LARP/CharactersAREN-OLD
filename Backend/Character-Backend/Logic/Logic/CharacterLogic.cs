using DAL.DALInterfaces;
using DAL.Exceptions;
using Logic.LogicInterfaces;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public class CharacterLogic : ICharacterLogic
    {
        private readonly ICharacterRepository _repo;
        private readonly ICareerLogic _careerLogic;

        public CharacterLogic(ICharacterRepository repo, ICareerLogic careerLogic)
        {
            _repo = repo;
            _careerLogic = careerLogic;
        }
        public async Task<Character> DeleteCharacter(Character character)
        {
            return await _repo.DeleteEntity(character);
        }

        public async Task<Character> GetCharacter(int id)
        {
            var character = await _repo.GetCharacterById(id);

            return character;
        }
        public async Task<IEnumerable<Character>> GetUserCharacters(int userId)
        {
            return await _repo.GetUserCharacters(userId);
        }

        public async Task<IEnumerable<Character>> GetCharacters()
        {
            var chars = await _repo.GetCharacters();
            return chars;
        }

        public async Task<Character> PostCharacter(Character character)
        {
            var exExist = await _repo.CharacterExists(character.IcName);
            if (exExist)
            {
                throw new ObjectAlreadyExistsException(character.GetType().Name, character.IcName);
            }
            else
            {
                if (character.CareerId.HasValue)
                {
                    character.Career = await _careerLogic.GetCareer(character.CareerId.Value);
                    //character.Skills.Add(character.Career.Skill);
                }
                if (await ValidateSkills(character) && character.Skills.Count <= 4)
                {
                    return await _repo.AddEntity(new Character { CareerId = character.CareerId, Events = character.Events, IcName = character.IcName, Skills = character.Skills, Status = character.Status, UserId = character.UserId, Backstory = character.Backstory });
                }
                else
                {
                    throw new ArgumentException("Skills are not valid for this character");
                }
            }
        }

        public async Task<Character> PutCharacter(int id, Character character, bool isAdmin)
        {
            if (character.Id != id)
            {
                throw new ArgumentException($"{nameof(PutCharacter)}: id in url is not equal to character id.");
            }

            if (isAdmin ? true : await ValidateSkills(character))
            {
                return await _repo.PutEntity(character);
            }
            else
            {
                throw new ArgumentException("Skills are not valid for this character");
            }
        }

        private async Task<bool> ValidateSkills(Character character)
        {
            if (!await AreCareerSkillsValid(character))
            {
                return false;
            }

            var skillsByCategory = character.Skills.GroupBy(s => s.SkillCategoryId);

            foreach (var skills in skillsByCategory)
            {
                if (!AreSkillLevelsValidForCategory(skills.ToList()))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<IEnumerable<Skill>> GetCharacterSkills(int characterId)
        {
            return await _repo.GetCharacterSkills(characterId);
        }

        private static bool AreSkillLevelsValidForCategory(List<Skill> skills)
        {
            var skillLevelLists = skills.GroupBy(s => s.Level).Select(s => new { SkillLevel = s.Key, SkillCount = s.Count() }).OrderBy(s => s.SkillLevel);
            int skillCount = 0;
            int skillLevelAllowed = 1;
            foreach (var skillList in skillLevelLists)
            {
                if (skillList.SkillLevel <= skillLevelAllowed)
                {
                    skillCount += skillList.SkillCount;
                    skillLevelAllowed = (skillCount / 3) + 1;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> AreCareerSkillsValid(Character character)
        {
            try
            {
                // This list should contain all careerskills
                var careers = await _careerLogic.GetCareers();
                List<Skill> careerSkills = careers.Select(c => c.Skill).ToList();
                var isValidCareerSkill = true;
                var selectedCareerSkill = character.Skills.SingleOrDefault(s => careerSkills.Any(cs => cs.Id == s.Id));
                if (selectedCareerSkill != null)
                {
                    isValidCareerSkill = character.Career.Skill.Id == selectedCareerSkill.Id;
                }

                return isValidCareerSkill;

            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
