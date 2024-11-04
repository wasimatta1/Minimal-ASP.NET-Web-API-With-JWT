namespace Minimal_ASP.NET_With_JWT.JWT_Token
{
    public interface IJwtTokenService
    {
        public JWTTokens GenerateToken(User user);

        public string ValidateToken(string token);
    }
}
