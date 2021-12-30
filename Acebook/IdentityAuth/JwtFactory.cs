using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Acebook.IdentityAuth;
using Acebook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Acebook.IdentityAuth
{
    public class JwtFactory
    {
        private const int TokenExpiryHours = 3;
        private readonly IConfiguration configuration;

        public JwtFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public TokenDto GenerateToken(ApplicationUser user, Guid tokenId, DateTime utcNow)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, tokenId.ToString()),
            };

            var authSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: this.configuration["JWT:ValidIssuer"],
                audience: this.configuration["JWT:ValidAudience"],
                expires: utcNow.AddHours(TokenExpiryHours),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var tokenDto = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
            };

            return tokenDto;
        }
    }
}
