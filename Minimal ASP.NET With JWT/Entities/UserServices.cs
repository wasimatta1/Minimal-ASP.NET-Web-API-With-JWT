using Minimal_ASP.NET_With_JWT.DbContexts;

namespace Minimal_ASP.NET_With_JWT.Entities
{
    public class UserServices : IUsersServices
    {
        private DbContextData _dbcontext;

        public UserServices(DbContextData dbContextData)
        {
            _dbcontext = dbContextData;
        }
        public IEnumerable<User> GetAll()
        {
            return _dbcontext.Users;
        }

    }
}
