using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Acebook.DbContext;
using Acebook.IdentityAuth;
using Acebook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TestSupport.EfHelpers;
using Xunit;

namespace Acebook.IntegrationTests.PostsRequests
{
    public class CreatePost : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> factory;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CreatePost(TestingWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.dbContext = this.factory.Services.GetService<ApplicationDbContext>();
            this.dbContext.Database.EnsureClean();
            this.userManager = this.factory.Services.GetService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async void CreatesAPost()
        {
            // Arrange
            var client = this.factory.CreateClient();
            var user = new ApplicationUser { UserName = "fred" };
            await this.userManager.CreateAsync(user, "Password123$");
            await RequestHelpers.Login(client, user, "Password123$");

            // Act
            var response = await client.PostAsync("/api/posts", new StringContent(
                JsonSerializer.Serialize(new Post { Body = "Hello World" }),
                Encoding.UTF8,
                "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(1, this.dbContext.Posts.Count());
            Assert.Equal("Hello World", this.dbContext.Posts.OrderBy(p => p.Id).First().Body);
        }
    }
}
