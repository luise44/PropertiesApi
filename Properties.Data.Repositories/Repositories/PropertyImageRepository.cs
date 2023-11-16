using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;

namespace Properties.Data.Repositories.Repositories
{
    public class PropertyImageRepository : BaseRepository<PropertyImage>, IPropertyImageRepository
    {
        public PropertyImageRepository(PropertiesDbContext dbContext) : base(dbContext) { }
    }
}
