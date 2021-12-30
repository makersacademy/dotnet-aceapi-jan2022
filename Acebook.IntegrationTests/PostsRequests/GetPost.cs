using System.Collections.Generic;
using System.Text.Json;
using Acebook.DbContext;
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
        private readonly TestingWebApplicationFactory<Startup> factory;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public GetPost(TestingWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.dbContext = this.factory.Services.GetService<ApplicationDbContext>();
            this.dbContext.Database.EnsureClean();
            this.userManager = this.factory.Services.GetService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async void GetsSinglePost()
        {
            // Arrange
            var client = this.factory.CreateClient();
            var user = new ApplicationUser { UserName = "fred" };
            await this.userManager.CreateAsync(user, "Password123$");
            await RequestHelpers.Login(client, user, "Password123$");
            var post1 = new Post { UserId = user.Id, Body = "Hello World" };
            var post2 = new Post { UserId = user.Id, Body = "Hello World 2" };
            this.dbContext.Posts.AddRange(post1, post2);
            await this.dbContext.SaveChangesAsync();

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
