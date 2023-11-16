using AutoMapper;
using Properties.Client.Models;
using Properties.Services.DTO;

namespace Properties.Util.Automapper.Profiles
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<LoginModel, UserDto>();
        }
    }
}