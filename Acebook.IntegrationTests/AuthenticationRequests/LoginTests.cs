using System.Net.Http;
using Acebook.DbContext;
using Acebook.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestSupport.EfHelpers;
using Xunit;

namespace Acebook.IntegrationTests.AuthenticationRequests
{
  public class LoginTests : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> factory;
        private readonly ApplicationDbContext dbContext;

        public LoginTests(TestingWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.dbContext = this.factory.Services.GetService<ApplicationDbContext>();
            this.dbContext.Database.EnsureClean();
        }

        [Fact]
        public async void LogsInWhenRegistered()
        {
            // Register
            var client = this.factory.CreateClient();
            await client.PostAsync(
                "/api/authenticate/register",
                new StringContent(
                    "{ \"username\": \"fred\", \"password\": \"Password123$\" }",
                    System.Text.Encoding.UTF8,
                    "application/json"));

            // Log in
            var response = await client.PostAsync(
                "/api/authenticate/login",
                new StringContent(
                    "{ \"username\": \"fred\", \"password\": \"Password123$\" }",
                    System.Text.Encoding.UTF8,
                    "application/json"));
            var tokenDto = JsonConvert.DeserializeObject<TokenDto>(await response.Content.ReadAsStringAsync());

            // Attempt to use token to access protected resource
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenDto.Token}");
            var protectedResponseWithToken = await client.GetAsync("/api/authenticate/status");
            protectedResponseWithToken.EnsureSuccessStatusCode();
        }
    }
}
