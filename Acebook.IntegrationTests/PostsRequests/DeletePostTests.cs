using System.Linq;
using Acebook.DbContext;
using Acebook.IdentityAuth;
using Acebook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TestSupport.EfHelpers;
using Xunit;

namespace Acebook.IntegrationTests.PostsRequests
{
  public class DeletePostTests : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> factory;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public DeletePostTests(TestingWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.dbContext = this.factory.Services.GetService<ApplicationDbContext>();
            this.dbContext.Database.EnsureClean();
            this.userManager = this.factory.Services.GetService<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async void DeletesPost()
        {
            var client = this.factory.CreateClient();
            var user = new ApplicationUser { UserName = "fred" };
            await this.userManager.CreateAsync(user, "Password123$");
            await RequestHelpers.Login(client, user, "Password123$");
            var post1 = new Post { UserId = user.Id, Body = "Hello World" };
            var post2 = new Post { UserId = user.Id, Body = "Hello World 2" };
            this.dbContext.Posts.AddRange(post1, post2);
            await this.dbContext.SaveChangesAsync();

            var response = await client.DeleteAsync($"/api/posts/{post2.Id}");

            response.EnsureSuccessStatusCode();
            Assert.Equal(1, this.dbContext.Posts.Count());
            Assert.Equal("Hello World", this.dbContext.Posts.OrderBy(p => p.Id).First().Body);
        }
    }
}
