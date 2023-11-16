using Properties.Services.DTO;

namespace Properties.Services.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDto userDto);

        UserDto GetUserByEmail(string email);

        UserDto GetUserById(int userId);
    }
}
