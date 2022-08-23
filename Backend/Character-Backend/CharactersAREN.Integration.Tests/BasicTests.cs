using CharactersAREN.Integration.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace CharactersAREN.Integration.Tests
{
    public class BasicTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly List<Claim> ReadAccess = new List<Claim>()
        {
            new Claim("permissions", "read:careers"),
            new Claim("permissions", "read:character"),
            new Claim("permissions", "read:events"),
            new Claim("permissions", "read:skillcategories"),
            new Claim("permissions", "read:skills")
        };
        public BasicTests(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("careers")]
        [InlineData("characters")]
        [InlineData("events")]
        [InlineData("skillcategories")]
        [InlineData("skills")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            string jwtToken = MockJwtTokens.GenerateJwtToken(ReadAccess);
            // Act          
            var postRequest = new HttpRequestMessage(HttpMethod.Get, "/api/" + url);
            postRequest.Headers.Add("Authorization", $"Bearer {jwtToken}");                
            var response = await _client.SendAsync(postRequest);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
