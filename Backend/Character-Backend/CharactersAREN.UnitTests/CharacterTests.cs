using DAL.DALInterfaces;
using FluentAssertions;
using Logic.Logic;
using Model.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

namespace CharactersAREN.UnitTests
{
    public class CharacterTests
    {
        private readonly CharacterLogic _characterLogic;
        private readonly Mock<ICharacterRepository> _characterRepository;
        private readonly CareerLogic _careerLogic;
        private readonly Mock<ICareerRepository> _careerRepository;
        public CharacterTests()
        {
            _characterRepository = new Mock<ICharacterRepository>();
            _careerRepository = new Mock<ICareerRepository>();
            _careerLogic = new CareerLogic(_careerRepository.Object);
            _characterLogic = new CharacterLogic(_characterRepository.Object, _careerLogic);
        }

        [Fact]
        public async Task GetCharacter_ShouldReturnCharacter_WhenCharacterDoesExist()
        {
            var character = new Character
            {
                Id = 1,
                IcName = "test me"
            };
            _characterRepository.Setup(cr => cr.GetCharacterById(It.Is<int>(i => i == character.Id))).Returns(Task.FromResult(character));
            var pulledCharacter = await _characterLogic.GetCharacter(1);

            pulledCharacter.Should().NotBeNull();
            pulledCharacter.Should().Be(character);
        }

        [Fact]
        public async Task GetCharacter_ShouldReturnNull_WhenCharacterDoesNotExist()
        {
            var character = new Character
            {
                Id = 1,
                IcName = "test me"
            };
            _characterRepository.Setup(cr => cr.GetCharacterById(It.Is<int>(i => i == character.Id))).Returns(Task.FromResult(character));
            var pulledCharacter = await _characterLogic.GetCharacter(2);

            pulledCharacter.Should().BeNull();
        }

        [Fact]
        public async Task PutCharacter_ShouldUpdateCharacter()
        {
            int id = 1;
            int userId = 2;
            int careerId = 3;
            var TestIcName = "Test";
            var career = new Career
            {
                Id = careerId,
                Name = "testCareer",
                SkillId = 1
            };

            var careers = new List<Career> { career };
            var character = new Character
            {
                Id = id,
                IcName = "test me",
                UserId = userId,
                CareerId = careerId,
                Skills = new List<Skill>()
            };

            var newCharacter = new Character
            {
                Id = id,
                IcName = TestIcName,
                UserId = userId,
                CareerId = careerId,
                Skills = new List<Skill>()
            };

            _characterRepository.Setup(ch => ch.GetCharacterById(It.Is<int>(i => i == 1))).Returns(Task.FromResult(character));
            _characterRepository.Setup(ch => ch.PutEntity(It.Is<Character>(c => c.Id == id && c.IcName == TestIcName))).Returns(Task.FromResult(newCharacter));
            _careerRepository.Setup(ca => ca.GetCareers()).Returns(Task.FromResult(careers));

            var updated = await _characterLogic.PutCharacter(id, newCharacter, false);

            updated.IcName.Should().Be(newCharacter.IcName);
            _characterRepository.Verify(mock => mock.PutEntity(newCharacter), Times.Once());
        }

        [Fact]
        public async Task DeleteCharacter_ShouldDeleteCharacter()
        {

            var character = new Character
            {
                Id = 1,
                IcName = "test me",
            };

            _characterRepository.Setup(p => p.DeleteEntity(It.Is<Character>(i => i == character))).Returns(Task.FromResult(character));

            var deleted = await _characterLogic.DeleteCharacter(character);

            deleted.Should().Be(character);
            _characterRepository.Verify(mock => mock.DeleteEntity(character), Times.Once());
        }

        [Fact]
        public async Task DeleteCharacter_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var character = new Character
            {
                Id = 1,
                IcName = "test me",
            };
            var characterWrongId = new Character
            {
                Id = 2,
                IcName = "test me",
            };
            _characterRepository.Setup(p => p.DeleteEntity(It.Is<Character>(i => i == character))).Returns(Task.FromResult(character));
            var deleted = await _characterLogic.DeleteCharacter(characterWrongId);


            deleted.Should().BeNull();
        }

        [Fact]
        public async Task PostCharacter_ShouldBePosted()
        {

            int userId = 2;
            int careerId = 3;
            var career = new Career
            {
                Id = careerId,
                Name = "testCareer",
                SkillId = 1
            };

            var careers = new List<Career> { career };
            var character = new Character
            {

                IcName = "test me",
                UserId = userId,
                CareerId = careerId,
                Skills = new List<Skill>()
            };


            _characterRepository.Setup(ch => ch.AddEntity(It.Is<Character>(c => c.IcName == character.IcName))).Returns(Task.FromResult(character));
            _careerRepository.Setup(ca => ca.GetCareers()).Returns(Task.FromResult(careers));
            var created = await _characterLogic.PostCharacter(character);

            created.Should().Be(character);
        }
    }
}
