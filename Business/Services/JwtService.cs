using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services
{
    public class JwtTokenService
    {
        private  JwtSettings _jwtSettings = new JwtSettings 
        { 
            Audience = "http://localhost:5174",
            Issuer = "http://localhost:7237",
            Secret = "DQWHFFHEWBCWIUEYF1UHEWFBCWEIFUEWHBVFBWNICEUYFBWEWDWQOKXQSIUJODQCQBWYECIUHBYEWUQWXNQWCHBWINWEBHUWEWCWEXIUIBWCYWIOWUBE"
        };

        public JwtTokenService()
        {
                
        }

        public string GenerateToken(string userId, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
