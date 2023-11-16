using AutoMapper;
using Microsoft.Extensions.Logging;
using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;
using Properties.Services.Application.Interfaces;
using Properties.Services.DTO;

namespace Properties.Services.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OwnerService> _logger;

        public OwnerService(
            IOwnerRepository ownerRepository,
            IMapper mapper,
            ILogger<OwnerService> logger
        )
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public List<OwnerDto> GetAll()
        {
            List<OwnerDto> result = new List<OwnerDto>();

            try
            {
                result = (_ownerRepository
                            .GetAll())
                            .Select(x => _mapper.Map<OwnerDto>(x))
                            .ToList();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error creating OWNER.");
                throw;
            }

            return result;
        }

        public async Task CreateOwner(OwnerDto ownerDto)
        {
            try
            {
                var owner = _mapper.Map<Owner>(ownerDto);
                _ownerRepository.Add(owner);
                await _ownerRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating OWNER.");
                throw;
            }
        }

        public OwnerDto GetOwnerById(int ownerId)
        {
            Owner owner;

            try
            {
                owner = _ownerRepository.GetById(ownerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting OWNER.");
                throw;
            }

            return _mapper.Map<OwnerDto>(owner);
        }
    }
}
