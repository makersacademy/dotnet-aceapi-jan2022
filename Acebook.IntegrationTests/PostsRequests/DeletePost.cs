using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Acebook.DBContext;
using Acebook.IdentityAuth;
using Acebook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TestSupport.EfHelpers;
using Xunit;

namespace Acebook.IntegrationTests.PostsRequests
{
    public class DeletePost : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> _factory;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeletePost(TestingWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _dbContext = _factory.Services.GetService<ApplicationDbContext>();
            _dbContext.Database.EnsureClean();
            _userManager = _factory.Services.GetService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async void DeletesPost()
        {
            // Arrange
            var client = _factory.CreateClient();
            var user = new ApplicationUser { UserName = "fred" };
            await _userManager.CreateAsync(user, "Password123$");
            await RequestHelpers.Login(client, user, "Password123$");
            var post1 = new Post { UserId = user.Id, Body = "Hello World" };
            var post2 = new Post { UserId = user.Id, Body = "Hello World 2" };
            _dbContext.Posts.AddRange(post1, post2);
            await _dbContext.SaveChangesAsync();

            // Act
            var response = await client.DeleteAsync($"/api/posts/{post2.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(1, _dbContext.Posts.Count());
            Assert.Equal("Hello World", _dbContext.Posts.OrderBy(p => p.Id).First().Body);
        }
    }
}
