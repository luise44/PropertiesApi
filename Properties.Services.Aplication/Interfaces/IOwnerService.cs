using Properties.Data.Entities;
using Properties.Data.Repositories;
using Properties.Services.DTO;

namespace Properties.Services.Application.Interfaces
{
    public interface IOwnerService
    {
        List<OwnerDto> GetAll();
        Task CreateOwner(OwnerDto ownerDto);
        OwnerDto GetOwnerById(int ownerId);
    }
}
