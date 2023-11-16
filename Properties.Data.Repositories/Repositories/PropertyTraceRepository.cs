using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;

namespace Properties.Data.Repositories.Repositories
{
    public class PropertyTraceRepository : BaseRepository<PropertyTrace>, IPropertyTraceRepository
    {
        public PropertyTraceRepository(PropertiesDbContext dbContext) : base(dbContext) { }
    }
}
