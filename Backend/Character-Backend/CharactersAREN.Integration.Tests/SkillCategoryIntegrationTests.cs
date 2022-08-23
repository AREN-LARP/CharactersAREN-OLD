using CharactersAREN.Integration.Tests.Helpers;
using Model.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace CharactersAREN.Integration.Tests
{
    public class SkillCategoryIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly List<Claim> WriteAccess = new List<Claim>()
        {
            new Claim("permissions", "create:skillcategories") 
        };
        private readonly HttpClient _client;
        public SkillCategoryIntegrationTests(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_SkillCategory_ReturnsSuccessAndSkillCategory()
        {
            string expected = "Mutant";
            string jwtToken = MockJwtTokens.GenerateJwtToken(WriteAccess);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/api/SkillCategories");
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");
            var content = new SkillCategory { Name = expected };

            postRequest.Content = new StringContent(JsonConvert.SerializeObject(content), System.Text.Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(postRequest);

            response.EnsureSuccessStatusCode();

            SkillCategory skillCategory = (SkillCategory)await response.Content.ReadFromJsonAsync(typeof(SkillCategory));
            Assert.Equal(expected, skillCategory.Name);
        }
    }
}
