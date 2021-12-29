using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Acebook.DBContext;
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
        private readonly TestingWebApplicationFactory<Startup> _factory;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreatePost(TestingWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _dbContext = _factory.Services.GetService<ApplicationDbContext>();
            _dbContext.Database.EnsureClean();
            _userManager = _factory.Services.GetService<UserManager<ApplicationUser>>();
        }


        [Fact]
        public async void CreatesAPost()
        {
            // Arrange
            var client = _factory.CreateClient();
            var user = new ApplicationUser { UserName = "fred" };
            await _userManager.CreateAsync(user, "Password123$");
            await RequestHelpers.Login(client, user, "Password123$");

            // Act
            var response = await client.PostAsync("/api/posts", new StringContent(
                JsonSerializer.Serialize(new Post { Body = "Hello World" }),
                Encoding.UTF8,
                "application/json"));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(1, _dbContext.Posts.Count());
            Assert.Equal("Hello World", _dbContext.Posts.OrderBy(p => p.Id).First().Body);
        }
    }
}
