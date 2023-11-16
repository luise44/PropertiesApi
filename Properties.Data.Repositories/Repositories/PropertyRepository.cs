using Microsoft.EntityFrameworkCore;
using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;

namespace Properties.Data.Repositories.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        private readonly PropertiesDbContext _propertiesDbContext;

        public PropertyRepository(PropertiesDbContext dbContext) : base(dbContext)
        {
            _propertiesDbContext = dbContext;
    }

        public List<Property> GetPropertiesFiltered(string name, string address, int? year, int? ownerId)
        {

            var a = _propertiesDbContext
                .Set<Property>()
                .Include(p => p.Owner).ToList();

            var propertiesQuery = _propertiesDbContext
                .Set<Property>()
                .Include(p => p.Owner)
                .Where(x => (!string.IsNullOrEmpty(name) && x.Name.Contains(name))
                        || (!string.IsNullOrEmpty(address) && x.Address.Contains(address))
                        || (year.HasValue && x.Year == year)
                        || (ownerId.HasValue && x.Owner.Id == ownerId)
                );

            return propertiesQuery.ToList();
        }
    }
}
