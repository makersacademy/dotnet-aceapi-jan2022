using System;
using Acebook.IdentityAuth;
using Acebook.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Acebook.UnitTests
{
  public class JwtFactoryTests
    {
        private const string TestingSecret =
            "RTSlzmc0+HagKxnwxiJMYPaiWEDSe76y5djd3PtWXBdkUyghVefzdbdI+OXt1qu4" +
            "jvev5iOtT57VCIhZSOrEpAR+zbe7dZRCUeZyQeqGepHwgJvOjqKYYUgCqYswQsX6" +
            "Mjp760m9GkGivFtqyq9vSpCHm2EdV8lTW9j7zaCH1UgTL9JTKRNYPhjYih2Rnu7+" +
            "IWu2D0QJna/lQ7a49ejcp+g4HdbttpOjbr35CA==";

        [Fact]
        public void GenerateTokenReturnsAToken()
        {
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(x => x["JWT:Secret"]).Returns(TestingSecret);
            configurationMock.Setup(x => x["JWT:ValidIssuer"]).Returns("issuer");
            configurationMock.Setup(x => x["JWT:ValidAudience"]).Returns("audience");
            var jwtFactory = new JwtFactory(configurationMock.Object);
            var user = new ApplicationUser()
            {
                Id = "fc0dcc69-d6df-435a-a9e0-a0b00afe1352",
                UserName = "testUserName",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var tokenId = Guid.Parse("054ce858-e899-40d3-894b-2aabb81f2bda");
            var fixedTime = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            TokenDto token = jwtFactory.GenerateToken(user, tokenId, fixedTime);

            Assert.Equal(
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5" +
                "4bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjo" +
                "idGVzdFVzZXJOYW1lIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3M" +
                "vMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJmYzB" +
                "kY2M2OS1kNmRmLTQzNWEtYTllMC1hMGIwMGFmZTEzNTIiLCJqdGkiOiIwNTR" +
                "jZTg1OC1lODk5LTQwZDMtODk0Yi0yYWFiYjgxZjJiZGEiLCJleHAiOjE1Nzc" +
                "4NDc2MDAsImlzcyI6Imlzc3VlciIsImF1ZCI6ImF1ZGllbmNlIn0.LzBzXRa" +
                "l9jnZ7U8RpVgD_JGKP3WI89cVaW4qI20mwSA", token.Token);
            Assert.Equal(fixedTime.AddHours(3), token.Expiration);
        }
    }
}
