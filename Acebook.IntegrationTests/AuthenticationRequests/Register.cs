using System.Net;
using System.Net.Http;
using Acebook.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestSupport.EfHelpers;
using Xunit;

namespace Acebook.IntegrationTests.AuthenticationRequests
{
    public class Register : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> factory;
        private readonly ApplicationDbContext dbContext;

        public Register(TestingWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.dbContext = this.factory.Services.GetService<ApplicationDbContext>();
            this.dbContext.Database.EnsureClean();
        }

        // POST /api/Authenticate/Register
        // { username: "username", password: "password" }
        [Fact]
        public async void CreatesUser()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.PostAsync(
                "/api/authenticate/register",
                new StringContent(
                    "{ \"username\": \"fred\", \"password\": \"Password123$\" }",
                    System.Text.Encoding.UTF8,
                    "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(
                "application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            Assert.Equal(
                "{\"status\":\"Success\",\"message\":\"User created successfully\"}",
                await response.Content.ReadAsStringAsync());

            var user = await this.dbContext.Users.FirstOrDefaultAsync(u => u.UserName == "fred");
            Assert.Equal("fred", user.UserName);
        }
    }
}
