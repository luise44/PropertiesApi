using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;

namespace Properties.Data.Repositories.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(PropertiesDbContext dbContext) : base(dbContext) { }
    }
}
