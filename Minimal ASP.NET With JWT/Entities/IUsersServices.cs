namespace Minimal_ASP.NET_With_JWT.Entities
{
    public interface IUsersServices
    {
        IEnumerable<User> GetAll();
    }
}
