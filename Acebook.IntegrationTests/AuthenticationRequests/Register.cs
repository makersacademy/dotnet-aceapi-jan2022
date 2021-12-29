using System.Net;
using System.Net.Http;
using Acebook.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestSupport.EfHelpers;
using Xunit;

namespace Acebook.IntegrationTests.AuthenticationRequests
{
    public class Register : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> _factory;
        private readonly ApplicationDbContext _dbContext;

        public Register(TestingWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _dbContext = _factory.Services.GetService<ApplicationDbContext>();
            _dbContext.Database.EnsureClean();
        }

        // POST /api/Authenticate/Register
        // { username: "username", password: "password" }
        [Fact]
        public async void CreatesUser()
        {
            // Arrange
            var client = _factory.CreateClient();

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

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "fred");
            Assert.Equal("fred", user.UserName);
        }
    }
}
