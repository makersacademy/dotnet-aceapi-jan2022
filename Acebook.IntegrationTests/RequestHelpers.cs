using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Acebook.IdentityAuth;
using Acebook.Models;

namespace Acebook.IntegrationTests
{
    public static class RequestHelpers
    {
        public static async Task<HttpClient> Login(HttpClient client, ApplicationUser user, string password)
        {
            var response = await client.PostAsync("/api/authenticate/login",
                new StringContent(JsonSerializer.Serialize(new LoginDto { Username = user.UserName, Password = password }),
                    System.Text.Encoding.UTF8, "application/json"));

            var tokenDto = JsonSerializer.Deserialize<TokenDto>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {tokenDto.Token}");
            return client;
        }
    }
}
