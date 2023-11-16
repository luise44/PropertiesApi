using AutoMapper;
using Properties.Client.Api.Models;
using Properties.Services.DTO;

namespace Properties.Client.Api.Automapper
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<LoginModel, UserDto>();
            CreateMap<OwnerModel, OwnerDto>()
                .ForMember(x=> x.Photo, opt => opt.Ignore());
            CreateMap<PropertyModel, PropertyDto>();
            CreateMap<PropertyImageModel, PropertyImageDto>()
                .ForMember(x=> x.File, opt=> opt.Ignore());

            CreateMap<PropertyImageDto, PropertyImageModel>()
                .ForMember(x => x.File, opt => opt.Ignore());
            CreateMap<OwnerDto, OwnerModel>()
                .ForMember(x => x.Photo, opt => opt.Ignore());
            CreateMap<PropertyDto, PropertyModel>();
        }
    }
}