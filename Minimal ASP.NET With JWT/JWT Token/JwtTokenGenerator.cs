using Microsoft.IdentityModel.Tokens;
using Minimal_ASP.NET_With_JWT.DbContexts;
using Minimal_ASP.NET_With_JWT.DTO;
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

        public JWTTokens GenerateToken(UserTokenDto user)
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

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWTToken:Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                return true;

            }
            catch
            {

                return false;
            }
        }

    }
}
