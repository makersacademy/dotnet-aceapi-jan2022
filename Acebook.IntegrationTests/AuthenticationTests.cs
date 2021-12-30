using System.Net;
using System.Net.Http;
using Acebook.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestSupport.EfHelpers;
using Xunit;

namespace Acebook.IntegrationTests
{
    public class AuthenticationTests : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> factory;
        private readonly ApplicationDbContext dbContext;

        public AuthenticationTests(TestingWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.dbContext = this.factory.Services.GetService<ApplicationDbContext>();
            this.dbContext.Database.EnsureClean();
        }

        // POST /api/Authenticate/Register
        // { username: "username", password: "password" }
        [Fact]
        public async void TestRegistrationCreatesUser()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/authenticate/register",
                new StringContent("{ \"username\": \"fred\", \"password\": \"Password123$\" }",
                    System.Text.Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            Assert.Equal("{\"status\":\"Success\",\"message\":\"User created successfully\"}",
                await response.Content.ReadAsStringAsync());

            var user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "fred");
            Assert.Equal("fred", user.UserName);
        }

        // GET /api/Authenticate/validate
        [Fact]
        public async void TestValidationReturnsForbiddenIfUnauthenticated()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/authenticate/status");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        // POST /api/Authenticate/Login
        // { username: "fred", password: "Password123$" }
        [Fact]
        public async void TestRegistrationFlowAllowsAccess()
        {
            // Arrange
            var client = this.factory.CreateClient();
            await client.PostAsync("/api/authenticate/register",
                new StringContent("{ \"username\": \"fred\", \"password\": \"Password123$\" }",
                    System.Text.Encoding.UTF8, "application/json"));

            // Act
            var response = await client.PostAsync("/api/authenticate/login",
                new StringContent("{ \"username\": \"fred\", \"password\": \"Password123$\" }",
                    System.Text.Encoding.UTF8, "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            var tokenDto = JsonConvert.DeserializeObject<TokenDto>(await response.Content.ReadAsStringAsync());

            // Attempt to use token to access protected resource
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenDto.Token}");
            var protectedResponseWithToken = await client.GetAsync("/api/authenticate/status");
            protectedResponseWithToken.EnsureSuccessStatusCode();
        }

        private class TokenDto
        {
            public string Token { get; set; }
            public string Expiration { get; set; }
        }
    }
}
