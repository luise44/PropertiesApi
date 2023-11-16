using AutoMapper;
using Microsoft.Extensions.Logging;
using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;
using Properties.Services.Application.Interfaces;
using Properties.Services.DTO;

namespace Properties.Services.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateUser(UserDto userDto)
        {
            try
            {
                if (_userRepository.Find(x => x.Email == userDto.Email).Any())
                {
                    throw new Exception("User already exists");
                }

                var user = _mapper.Map<User>(userDto);

                _userRepository.Add(user);
                await _userRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating a new USER");
                throw;
            }
        }

        public UserDto GetUserByEmail(string email)
        {
            UserDto userDto;

            try
            {
                var user = _userRepository
                    .Find(x => x.Email == email)
                    .FirstOrDefault();

                userDto = _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting a new USER by Email");
                throw;
            }

            return userDto;
        }

        public UserDto GetUserById(int userId)
        {
            UserDto userDto;

            try
            {
                var user = _userRepository.GetById(userId);

                userDto = _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting a new USER by Email");
                throw;
            }

            return userDto;
        }
    }
}
