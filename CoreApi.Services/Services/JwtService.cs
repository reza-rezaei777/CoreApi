using CoreApi.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreApi.Services.Services
{
    public class JwtService : IJwtService
    {
        public string Generate(User user)
        {
            var securitykey = Encoding.UTF8.GetBytes("mySecretkey12345678922");// longer than 16 character
            var signingcredentials = new SigningCredentials(new SymmetricSecurityKey(securitykey), SecurityAlgorithms.HmacSha256Signature);
            var cliams = _getClaims(user);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = "myself",
                Audience = "myself",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = signingcredentials,
                Subject = new ClaimsIdentity(cliams)
            };
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var securitytoken = handler.CreateToken(descriptor);
            var token = handler.WriteToken(securitytoken);
            return token;

        }
        private IEnumerable<Claim> _getClaims(User user)
        {
            var list = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
            };
            var roles = new Role[] { new Role { Name = "Admin" } };
            foreach (var role in roles)
            {
                list.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            return list;
        }
    }
}
