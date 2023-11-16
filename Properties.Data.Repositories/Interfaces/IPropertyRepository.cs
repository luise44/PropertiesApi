using Properties.Data.Entities;
using Properties.Data.Repositories.Repositories;

namespace Properties.Data.Repositories.Interfaces
{
    public interface IPropertyRepository: IBaseRepository<Property>
    {
        List<Property> GetPropertiesFiltered(string name, string address, int? year, int? ownerId);
    }
}
