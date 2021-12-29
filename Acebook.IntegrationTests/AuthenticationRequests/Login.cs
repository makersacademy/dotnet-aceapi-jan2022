using System.Net;
using System.Net.Http;
using Acebook.DBContext;
using Acebook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestSupport.EfHelpers;
using Xunit;

namespace Acebook.IntegrationTests.AuthenticationRequests
{
    public class Login : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> _factory;
        private readonly ApplicationDbContext _dbContext;

        public Login(TestingWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _dbContext = _factory.Services.GetService<ApplicationDbContext>();
            _dbContext.Database.EnsureClean();
        }

        // POST /api/Authenticate/Login
        // { username: "fred", password: "Password123$" }
        [Fact]
        public async void LogsInWhenRegistered()
        {
            // Arrange
            var client = _factory.CreateClient();
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
    }
}
