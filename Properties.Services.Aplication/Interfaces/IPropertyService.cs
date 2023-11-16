using Properties.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Services.Application.Interfaces
{
    public interface IPropertyService
    {
        List<PropertyDto> GetAll();
        Task CreateProperty(PropertyDto propertyDto);
        Task UpdatePropertyPrice(int propertyId, float newPrice);
        Task UpdateProperty(int propertyId, PropertyDto propertyDto);
        Task AddImage(PropertyImageDto propertyImageDto);
        List<PropertyDto> GetFiltered(PropertyFilterDto propertyFilterDto);
        PropertyDto GetById(int id);
    }
}
