using CoreApi.Domin;
using CoreApi.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoreApi.Services.Services
{
    public class JwtService : IJwtService
    {
        private readonly SiteSettings _siteSettings;
        public JwtService(IOptionsSnapshot<SiteSettings> options)
        {
            _siteSettings=options.Value;
        }
        public string Generate(User user)
        {
            var securitykey = Encoding.UTF8.GetBytes(_siteSettings.JWTSettings.SecretKey);// longer than 16 character
            var signingcredentials = new SigningCredentials(new SymmetricSecurityKey(securitykey), SecurityAlgorithms.HmacSha256Signature);
            var encriptkey = Encoding.UTF8.GetBytes(_siteSettings.JWTSettings.EncrypKey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encriptkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            
            var cliams = _getClaims(user);
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSettings.JWTSettings.Issuer,
                Audience = _siteSettings.JWTSettings.Audience,
                IssuedAt =DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSettings.JWTSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSettings.JWTSettings.ExpirationMinutes),
                SigningCredentials = signingcredentials,
                EncryptingCredentials=encryptingCredentials,
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
