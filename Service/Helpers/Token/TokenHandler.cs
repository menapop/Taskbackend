using Data.Entities;
using DTOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repo;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service.Helpers.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly AppSettings _appSettings;
        
        public TokenHandler(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
          
        }
        public OutputRefreshTokenDto GetToken(User user)
        {

            if (user == null)
                   return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescription = new SecurityTokenDescriptor()
            {
                //Audience=_appSettings.Audience,
                //Issuer=_appSettings.Issuer,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                    new Claim("Roles", String.Join (",", user.UserRoles.Select(r=>r.RoleId).ToList()))
                })
            };
            var token = tokenHandler.CreateToken(tokenDescription);

            return new OutputRefreshTokenDto()
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = BuildRefreshToken()
            };
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims;
        }
        private string BuildRefreshToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
   
            }
        }

    }
}
