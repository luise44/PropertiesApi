using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;
using Properties.Services.Application.Interfaces;
using Properties.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Services.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PropertyService> _logger;

        public  PropertyService(
            IPropertyRepository propertyRepository,
            IPropertyImageRepository propertyIamgeRepository,
            IMapper mapper,
            ILogger<PropertyService> logger
        )
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _propertyImageRepository = propertyIamgeRepository;
            _logger = logger;
        }

        public async Task CreateProperty(PropertyDto propertyDto)
        {
            try
            {
                var property = _mapper.Map<Property>(propertyDto);

                _propertyRepository.Add(property);
                await _propertyRepository.SaveChanges();
            }
            catch( Exception ex )
            {
                _logger.LogError(ex, "Error creating a PROPERTY");
                throw;
            }
        }

        public PropertyDto GetById(int id)
        {
            PropertyDto propertyDto; 

            try
            {
                var property = _propertyRepository.GetById(id);

                propertyDto = _mapper.Map<PropertyDto>(property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting a PROPERTY");
                throw;
            }
            
            return propertyDto;
        }

        public List<PropertyDto> GetAll()
        {
            List<PropertyDto> properties;

            try
            {
                properties = (_propertyRepository
                    .GetAll())
                    .Select(x => _mapper.Map<PropertyDto>(x))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all PROPERTIES");
                throw;
            }

            return properties;
        }

        public async Task UpdatePropertyPrice(int propertyId, float newPrice)
        {
            try
            {
                var property = _propertyRepository.GetById(propertyId);

                if (property == null)
                {
                    throw new ArgumentException("Property not Found");
                }

                property.Price = newPrice;

                _propertyRepository.Update(property);
                await _propertyRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating a PROPERTY price");
                throw;
            }
        }

        public async Task UpdateProperty(int propertyId, PropertyDto propertyDto)
        {
            try
            {
                var property = _propertyRepository.GetById(propertyId);

                if (property == null)
                {
                    throw new ArgumentException("Property not Found");
                }

                property.Address = propertyDto.Address;
                property.Price = propertyDto.Price;
                property.Year = propertyDto.Year;

                _propertyRepository.Update(property);
                await _propertyRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating a PROPERTY");
                throw;
            }
        }

        public async Task AddImage(PropertyImageDto propertyImageDto)
        {
            try
            {
                var property = _propertyRepository.GetById(propertyImageDto.PropertyId);

                if (property == null)
                {
                    throw new ArgumentException("Property not Found");
                }

                var propertyImage = _mapper.Map<PropertyImage>(propertyImageDto);
                propertyImage.Property = property;

                _propertyImageRepository.Add(propertyImage);
                await _propertyImageRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding image to PROPERTY");
                throw;
            }
        }

        public List<PropertyDto> GetFiltered(PropertyFilterDto propertyFilterDto)
        {
            List<PropertyDto> properties;

            try
            {
                properties = _propertyRepository
                    .GetPropertiesFiltered(
                        propertyFilterDto.Name,
                        propertyFilterDto.Address,
                        propertyFilterDto.OwnerId,
                        propertyFilterDto.Year)
                    .Select(x => _mapper.Map<PropertyDto>(x))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting filtered PROPERTIES");
                throw;
            }

            return properties;
        }
    }
}
