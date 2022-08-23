using DAL.DALInterfaces;
using FluentAssertions;
using Logic.Logic;
using Model.Models;
using Moq;
using System;
using System.Threading.Tasks;

using Xunit;

namespace CharactersAREN.UnitTests
{
    public class SkillCategoryTests
    {
        private readonly SkillCategoryLogic _logic;
        private readonly Mock<ISkillCategoryRepository> _skillCategoryRepository;
        public SkillCategoryTests()
        {
            _skillCategoryRepository = new Mock<ISkillCategoryRepository>();

            _logic = new SkillCategoryLogic(_skillCategoryRepository.Object);
        }

        [Fact]
        public async Task GetSkillCategory_ShouldReturnSkillCategory_WhenSkillCategoryDoesExist()
        {
            var skillCategory = new SkillCategory
            {
                Id = 1,
                Name = "test me",              
            };
            _skillCategoryRepository.Setup(sr => sr.GetSkillCategoryById(It.Is<int>(i => i == skillCategory.Id))).Returns(Task.FromResult(skillCategory));
            var pulledSkillCategory = await _logic.GetSkillCategory(1);

            pulledSkillCategory.Should().NotBeNull();
            pulledSkillCategory.Should().Be(skillCategory);
        }

        [Fact]
        public async Task GetSkillCategory_ShouldReturnNull_WhenSkillCategoryDoesNotExist()
        {
            var skillCategory = new SkillCategory
            {
                Id = 1,
                Name = "test me",
            };
            _skillCategoryRepository.Setup(sr => sr.GetSkillCategoryById(It.Is<int>(i => i == skillCategory.Id))).Returns(Task.FromResult(skillCategory));
            var pulledSkillCategory = await _logic.GetSkillCategory(2);

            pulledSkillCategory.Should().BeNull();
        }

        [Fact]
        public async Task PutSkillCategory_ShouldUpdateSkillCategory()
        {
            int id = 1;
            var Name = "Engineer";

            var skillCategory = new SkillCategory
            {
                Id = id,
                Name = "test me"
            };

            var newSkillCategory = new SkillCategory
            {
                Id = id,
                Name = Name
            };

            _skillCategoryRepository.Setup(p => p.GetSkillCategoryById(It.Is<int>(i => i == 1))).Returns(Task.FromResult(skillCategory));
            _skillCategoryRepository.Setup(p => p.PutEntity(It.Is<SkillCategory>(sc => sc.Id == id && sc.Name == Name))).Returns(Task.FromResult(newSkillCategory));


            var updated = await _logic.PutSkillCategory(id, newSkillCategory);

            updated.Name.Should().Be(newSkillCategory.Name);
            _skillCategoryRepository.Verify(mock => mock.PutEntity(newSkillCategory), Times.Once());
        }

        [Fact]
        public async Task DeleteSkillCategory_ShouldDeleteSkillCategory()
        {
            var skillCategory = new SkillCategory
            {
                Id = 1,
                Name = "test me"
            };
            _skillCategoryRepository.Setup(p => p.DeleteEntity(It.Is<SkillCategory>(i => i == skillCategory))).Returns(Task.FromResult(skillCategory));
            var deleted = await _logic.DeleteSkillCategory(skillCategory);

            deleted.Should().Be(skillCategory);
            _skillCategoryRepository.Verify(mock => mock.DeleteEntity(skillCategory), Times.Once());
        }

        [Fact]
        public async Task DeleteSkillCategory_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var skillCategory = new SkillCategory
            {
                Id = 1,
                Name = "test me"
            };
            var skillCategory2 = new SkillCategory
            {
                Id = 2,
                Name = "test me"
            };
            _skillCategoryRepository.Setup(p => p.DeleteEntity(It.Is<SkillCategory>(i => i == skillCategory))).Returns(Task.FromResult(skillCategory));
            var deleted = await _logic.DeleteSkillCategory(skillCategory2);


            deleted.Should().BeNull();
        }

        [Fact]
        public async Task PostSkillCategory_ShouldBePosted()
        {
            var skillCategoryName = "test";
            int SkillCategoryId = 1;
            var newSkillCategory = new SkillCategory
            {
                Id = SkillCategoryId,
                Name = skillCategoryName,

            };


            _skillCategoryRepository.Setup(sr => sr.AddEntity(It.Is<SkillCategory>(sc => sc.Name == skillCategoryName))).Returns(Task.FromResult(newSkillCategory));
            var created = await _logic.PostSkillCategory(newSkillCategory);

            created.Should().Be(newSkillCategory);
        }
    }
}
