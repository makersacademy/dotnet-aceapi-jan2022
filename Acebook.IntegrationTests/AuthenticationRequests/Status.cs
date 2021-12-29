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
    public class AuthenticationTests : IClassFixture<TestingWebApplicationFactory<Startup>>
    {
        private readonly TestingWebApplicationFactory<Startup> _factory;
        private readonly ApplicationDbContext _dbContext;

        public AuthenticationTests(TestingWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _dbContext = _factory.Services.GetService<ApplicationDbContext>();
            _dbContext.Database.EnsureClean();
        }

        // GET /api/Authenticate/validate
        [Fact]
        public async void IsForbiddenIfUnauthenticated()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/authenticate/status");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
