using System.Collections.Generic;
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
    public class GetPost : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> _factory;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetPost(TestingWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _dbContext = _factory.Services.GetService<ApplicationDbContext>();
            _dbContext.Database.EnsureClean();
            _userManager = _factory.Services.GetService<UserManager<ApplicationUser>>();
        }


        [Fact]
        public async void GetsSinglePost()
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
            var response = await client.GetAsync($"/api/posts/{post2.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
            var responseString = await response.Content.ReadAsStringAsync();
            var postDto = JsonSerializer.Deserialize<PostDto>(responseString);
            Assert.Equal("Hello World 2", postDto.Body);
            Assert.Equal("fred", postDto.User.Username);
        }
    }
}
