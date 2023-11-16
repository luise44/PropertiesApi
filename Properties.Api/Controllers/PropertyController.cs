using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Properties.Client.Api.Models;
using Properties.Services.Application.Interfaces;
using Properties.Services.Application.Services;
using Properties.Services.DTO;

namespace Properties.Client.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyController(
            ILogger<PropertyController> logger,
            IPropertyService propertyService,
            IMapper mapper
        )
        {
            _logger = logger;
            _propertyService = propertyService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody]PropertyModel propertyModel)
        {
            try
            {
                var property = _mapper.Map<PropertyDto>(propertyModel);
                await _propertyService.CreateProperty(property);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was an error creating a new PROPERTY.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]PropertyModel propertyModel)
        {
            try
            {
                await _propertyService.UpdatePropertyPrice(propertyModel.Id, propertyModel.Price);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error updating PROPERTY price.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPut("price")]
        public async Task<IActionResult> UpdatePrice([FromBody] PropertyModel propertyModel)
        {
            try
            {
                var property = _mapper.Map<PropertyDto>(propertyModel);
                await _propertyService.UpdateProperty(propertyModel.Id, property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error updating PROPERTY.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            try
            {
                return Ok((_propertyService
                        .GetAll())
                        .Select(x=> _mapper.Map<PropertyModel>(x))
                        .ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error listing PROPERTIES.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("listFiltered")]
        public IActionResult ListFiltered([FromQuery]int? year, [FromQuery]int? ownerId, [FromQuery] string? name, [FromQuery] string? address)
        {
            try
            {
                var propertyFilterDto = new PropertyFilterDto
                {
                    Address = address,
                    Name = name,
                    Year = year,
                    OwnerId = ownerId
                };

                return Ok((_propertyService
                        .GetFiltered(propertyFilterDto))
                        .Select(x => _mapper.Map<PropertyModel>(x))
                        .ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error listing PROPERTIES.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("image")]
        public async Task<IActionResult> AddImage([FromForm] PropertyImageModel propertyImageModel)
        {
            try
            {
                var propertyImageDto = _mapper.Map<PropertyImageDto>(propertyImageModel);

                using (var stream = new MemoryStream())
                {
                    propertyImageModel.File.CopyTo(stream);
                    propertyImageDto.File = stream.ToArray();
                }

                await _propertyService.AddImage(propertyImageDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error listing PROPERTIES.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
