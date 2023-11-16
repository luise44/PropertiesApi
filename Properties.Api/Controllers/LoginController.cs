using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Properties.Client.Api.Models;
using Properties.Services.Application.Interfaces;
using Properties.Services.Authentication.Interfaces;
using Properties.Services.DTO;

namespace Properties.Client.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginController> _logger;

        public LoginController(
            IAuthenticationService authenticationService,
            IUserService userService,
            IMapper mapper,
            ILogger<LoginController> logger
        )
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] LoginModel loginModel)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(loginModel);

                await _userService.CreateUser(userDto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was an error registering a new USER.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            string token;

            try
            {
                var user = _userService.GetUserByEmail(loginModel.Email);

                if (user == null || user.Password != loginModel.Password)
                {
                    return Unauthorized();
                }

                token = _authenticationService.GetJwtToken();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "There was an error sign in the user.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok(token);
        }
    }
}
