using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Properties.Client.Api.Models;
using Properties.Services.Application.Interfaces;
using Properties.Services.DTO;

namespace Properties.Client.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly ILogger<OwnerController> _logger;
        private readonly IMapper _mapper;

        public OwnerController(
            IOwnerService ownerService,
            ILogger<OwnerController> logger,
            IMapper mapper
        )
        {
            _ownerService = ownerService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromForm]OwnerModel ownerModel)
        {
            try
            {
                var ownerDto = _mapper.Map<OwnerDto>(ownerModel);

                using (var stream = new MemoryStream())
                {
                    ownerModel.Photo.CopyTo(stream);
                    ownerDto.Photo = stream.ToArray();
                }

                await _ownerService.CreateOwner(ownerDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error creting a new OWNER.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            try
            {
                return Ok((_ownerService.GetAll())
                    .Select(x=> _mapper.Map<OwnerModel>(x))
                    .ToList());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "There was an error listing OWNERS.");
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
