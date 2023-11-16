using AutoMapper;
using Properties.Data.Entities;
using Properties.Services.DTO;

namespace Properties.Services.Application.Automapper
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<User, UserDto>();
            CreateMap<PropertyImage, PropertyImageDto>();

            CreateMap<PropertyDto, Property>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<UserDto, User>();
            CreateMap<PropertyImageDto, PropertyImage>();
        }
    }
}
