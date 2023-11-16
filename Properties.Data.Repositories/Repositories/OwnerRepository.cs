using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;

namespace Properties.Data.Repositories.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(PropertiesDbContext dbContext) : base(dbContext) { }
    }
}
