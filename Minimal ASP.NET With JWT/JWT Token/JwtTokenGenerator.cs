using Microsoft.IdentityModel.Tokens;
using Minimal_ASP.NET_With_JWT.DbContexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Minimal_ASP.NET_With_JWT.JWT_Token
{
    public class JwtTokenGenerator : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextData _context;

        public JwtTokenGenerator(IConfiguration configuration, DbContextData context)
        {
            _configuration = configuration;
            _context = context;
        }

        public JWTTokens GenerateToken(User user)
        {
            if (!_context.Users.Any(u => u.UserName == user.UserName && u.Password == user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTToken:Key"]));
            var ToeknDescp = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(ToeknDescp);

            return new JWTTokens
            {
                Token = tokenHandler.WriteToken(token)
            };
        }

        public string ValidateToken(string token)
        {
            throw new NotImplementedException();
        }

    }
}
