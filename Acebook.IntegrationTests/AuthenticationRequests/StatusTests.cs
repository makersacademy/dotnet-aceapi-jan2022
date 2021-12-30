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
    public class StatusTests : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> factory;
        private readonly ApplicationDbContext dbContext;

        public StatusTests(TestingWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.dbContext = this.factory.Services.GetService<ApplicationDbContext>();
            this.dbContext.Database.EnsureClean();
        }

        [Fact]
        public async void ReturnsForbiddenIfUnauthenticated()
        {
            var client = this.factory.CreateClient();

            var response = await client.GetAsync("/api/authenticate/status");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
