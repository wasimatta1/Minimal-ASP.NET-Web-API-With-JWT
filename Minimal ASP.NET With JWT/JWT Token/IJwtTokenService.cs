using Minimal_ASP.NET_With_JWT.DTO;

namespace Minimal_ASP.NET_With_JWT.JWT_Token
{
    public interface IJwtTokenService
    {
        public JWTTokens GenerateToken(UserTokenDto user);

        public bool ValidateToken(string token);
    }
}
