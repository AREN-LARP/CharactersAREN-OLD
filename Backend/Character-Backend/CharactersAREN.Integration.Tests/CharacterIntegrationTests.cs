using CharactersAREN.Integration.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CharactersAREN.Integration.Tests
{
    public class CharacterIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly List<Claim> WriteAccess = new List<Claim>()
        {
            new Claim("permissions", "create:character"),
            new Claim("permissions", "update:character")
        };
        private readonly List<Claim> DeleteAccess = new List<Claim>()
        {
            new Claim("permissions", "delete:character")
        };
        private readonly TestingWebAppFactory<Program> _factory;
        public CharacterIntegrationTests(TestingWebAppFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("Reyna", 1)]
        public async Task Create_Character_ReturnsSuccessAndCharacter(string icName, int userId)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(WriteAccess);
            string expected = icName;
            int expectedUser = userId;
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Characters");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var content = new Character { IcName = expected, UserId = expectedUser, Skills = new List<Skill>() };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            Character character = (Character)await response.Content.ReadFromJsonAsync(typeof(Character));
            Assert.Equal(expected, character.IcName);
        }

        [Theory]
        [InlineData("Jenna", 1)]
        public async Task Create_Character_ThrowsExceptionWhenCharacterExists(string icName, int userId)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(WriteAccess);
            string expected = icName;
            int expectedUser = userId;
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/Characters");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var content = new Character { IcName = expected, UserId = expectedUser, Skills = new List<Skill>() };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(postRequest);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Theory]
        [InlineData(1, 1, "Jenna Bartus")]
        public async Task Update_Character_ReturnsSucces(int userId, int characterId, string newIcName)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(WriteAccess);
            string expected = newIcName;
            int expectedUser = userId;
            var postRequest = new HttpRequestMessage(HttpMethod.Put, $"/api/Characters/{characterId}");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var content = new Character { Id = characterId, IcName = expected, UserId = expectedUser, Skills = new List<Skill>() };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            Character character = (Character)await response.Content.ReadFromJsonAsync(typeof(Character));
            Assert.Equal(expected, character.IcName);
        }

        [Theory]
        [InlineData(1, 2, "Jenna Bartus")]
        public async Task Update_Character_ThrowsExceptionWhenCharacterNotExists(int userId, int characterId, string newIcName)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(WriteAccess);
            string expected = newIcName;
            int expectedUser = userId;
            var postRequest = new HttpRequestMessage(HttpMethod.Put, $"/api/Characters/{characterId}");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var content = new Character { Id = characterId, IcName = expected, UserId = expectedUser, Skills = new List<Skill>() };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(postRequest);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [Theory]
        [InlineData(1, 1, 3, "Jenna Bartus")]
        public async Task Update_Character_ThrowsExceptionWhenCharacterNotEqualToId(int userId, int characterId, int notCharacterId, string newIcName)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(WriteAccess);
            string expected = newIcName;
            int expectedUser = userId;
            var postRequest = new HttpRequestMessage(HttpMethod.Put, $"/api/Characters/{characterId}");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var content = new Character { Id = notCharacterId, IcName = expected, UserId = expectedUser, Skills = new List<Skill>() };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(postRequest);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(1, 1, 3, "Jenna Bartus")]
        public async Task Update_Character_ThrowsExceptionWhenSkillLevelsAreInvalid(int userId, int characterId, int notCharacterId, string newIcName)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(WriteAccess);
            string expected = newIcName;
            int expectedUser = userId;
            var postRequest = new HttpRequestMessage(HttpMethod.Put, $"/api/Characters/{characterId}");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var content = new Character
            {
                Id = notCharacterId,
                IcName = expected,
                UserId = expectedUser,
                Skills = new List<Skill>() { new Skill { Id = 1, Name = "Blauwdrukken maken", Level = 1, SkillCategoryId = 1, Description = "Kek blauwdrukken maken" },
                    new Skill { Id = 3, Name = "Sleutelmaker", Level = 4, SkillCategoryId = 1, Description = "Wow skillsleutels maken" } }
            };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(postRequest);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(1, 1, 3, "Jenna Bartus")]
        public async Task Update_Character_ThrowsExceptionWhenCareerSkillsAreInvalid(int userId, int characterId, int notCharacterId, string newIcName)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(WriteAccess);
            string expected = newIcName;
            int expectedUser = userId;
            var postRequest = new HttpRequestMessage(HttpMethod.Put, $"/api/Characters/{characterId}");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");

            var content = new Character
            {
                Id = notCharacterId,
                IcName = expected,
                UserId = expectedUser,
                Skills = new List<Skill>() { new Skill { Id = 1, Name = "Blauwdrukken maken", Level = 1, SkillCategoryId = 1, Description = "Kek blauwdrukken maken" },
                    new Skill { Id = 2, Name = "Grondstof bewerken", Level = 1, SkillCategoryId = 2, Description = "Kek bewerken" } }
            };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync(postRequest);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(55)]
        public async Task Delete_Character_ReturnsSuccessAndCharacterDeleted(int characterId)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(DeleteAccess);
            var postRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/Characters/{characterId}");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            Character character = (Character)await response.Content.ReadFromJsonAsync(typeof(Character));
            Assert.Equal(characterId, character.Id);
        }

        [Theory]
        [InlineData(9191)]
        public async Task Delete_Character_ReturnsNotFoundWhenCharacterDoesNotExist(int characterId)
        {
            var client = _factory.GetClient();
            string jwtToken = MockJwtTokens.GenerateJwtToken(DeleteAccess);
            var postRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/Characters/{characterId}");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var response = await client.SendAsync(postRequest);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}