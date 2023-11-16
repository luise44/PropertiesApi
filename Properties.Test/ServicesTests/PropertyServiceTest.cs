using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Properties.Data;
using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;
using Properties.Data.Repositories.Repositories;
using Properties.Services.Application.Automapper;
using Properties.Services.Application.Services;
using Properties.Services.DTO;
using Properties.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Properties.Test.ServicesTests
{
    [TestClass]
    public class PropertyServiceTest
    {
        private IMapper mapper;
        private ILogger<PropertyService> logger;
        private List<Property> properties;

        [TestInitialize]
        public void Setup()
        {
            properties = new List<Property>
            {
                new Property
                {
                    Address = "Fake ST 1",
                    CodeInternal = "001",
                    Id = 1,
                    Name = "Property 1",
                    Price = 1.00f,
                    Year = 1
                },
                new Property
                {
                    Address = "Fake ST 2",
                    CodeInternal = "002",
                    Id = 2,
                    Name = "Property 2",
                    Price = 2.00f,
                    Year = 2 
                }
            };

            var mapperProfile = new ServicesProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            mapper = new Mapper(mapperConfiguration);

            logger = new Mock<ILogger<PropertyService>>().Object;
        }

        [TestMethod]
        public async Task Created_property_not_null()
        {
            var propertyRepository = new Mock<IPropertyRepository>();
            propertyRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Property
                {
                    Address = "Fake ST 3",
                    CodeInternal = "003",
                    Id = 3,
                    Name = "Property 3",
                    Price = 3.00f,
                    Year = 3
                });
            var propertyImageRepository = new Mock<IPropertyImageRepository>().Object;

            var newProperty = new PropertyDto
            {
                Address = "Fake ST 3",
                CodeInternal = "003",
                Id = 3,
                Name = "Property 3",
                Price = 3.00f,
                Year = 3
            };

            var propertyService = new PropertyService(
                propertyRepository.Object,
                propertyImageRepository,
                mapper,
                logger
            );

            await propertyService.CreateProperty(newProperty);

            var propertyCreated = propertyService.GetById(newProperty.Id);

            Assert.IsNotNull(propertyCreated);
            Assert.AreEqual(propertyCreated.Name, newProperty.Name);
            Assert.AreEqual(propertyCreated.Price, newProperty.Price);
            Assert.AreEqual(propertyCreated.CodeInternal, newProperty.CodeInternal);
            Assert.AreEqual(propertyCreated.Address, newProperty.Address);
        }

        [TestMethod]
        public void Get_By_Id_Not_Null()
        {
            var propertyRepository = new Mock<IPropertyRepository>();
            propertyRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Property
                {
                    Address = "Fake ST 3",
                    CodeInternal = "003",
                    Id = 3,
                    Name = "Property 3",
                    Price = 3.00f,
                    Year = 3
                });
            var propertyImageRepository = new Mock<IPropertyImageRepository>().Object;

            var propertyService = new PropertyService(
                propertyRepository.Object,
                propertyImageRepository,
                mapper,
                logger
            );

            var expectedId = 3;

            var propertyFromService = propertyService.GetById(expectedId);

            Assert.IsNotNull(propertyFromService);
            Assert.AreEqual(expectedId, propertyFromService.Id);
        }

        [TestMethod]
        public void Get_All_Not_Empty()
        {
            var expectedCount = 2;

            var propertyRepository = new Mock<IPropertyRepository>();
            propertyRepository
                .Setup(x => x.GetAll())
                .Returns(properties);

            var propertyImageRepository = new Mock<IPropertyImageRepository>().Object;

            var propertyService = new PropertyService(
                propertyRepository.Object,
                propertyImageRepository,
                mapper,
                logger
            );

            var propertiesFromService = propertyService.GetAll();

            Assert.AreEqual(expectedCount, propertiesFromService.Count);
        }

        [TestMethod]
        public async Task Property_Price_Updated_As_Expected()
        {
            var expectedPrice = 3.33f;
            var updatePropertyId = 3;

            var propertyRepository = new Mock<IPropertyRepository>();
            propertyRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Property
                {
                    Address = "Fake ST 3",
                    CodeInternal = "003",
                    Id = 3,
                    Name = "Property 3",
                    Price = 3.333f,
                    Year = 3
                });

            var propertyImageRepository = new Mock<IPropertyImageRepository>().Object;

            var propertyService = new PropertyService(
                propertyRepository.Object,
                propertyImageRepository,
                mapper,
                logger
            );

            await propertyService.UpdatePropertyPrice(updatePropertyId, expectedPrice);

            var propertyUpdated = propertyService.GetById(updatePropertyId);

            Assert.AreEqual(expectedPrice, propertyUpdated.Price);
        }

        [TestMethod]
        public async Task Property_Updated_As_Expected()
        {
            var expectedUpdated = new Property
            {
                Address = "Fake ST 3",
                CodeInternal = "003",
                Id = 3,
                Name = "Property 3",
                Price = 3.333f,
                Year = 3
            };

            var propertyRepository = new Mock<IPropertyRepository>();
            propertyRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(expectedUpdated);

            var propertyImageRepository = new Mock<IPropertyImageRepository>().Object;

            var propertyService = new PropertyService(
                propertyRepository.Object,
                propertyImageRepository,
                mapper,
                logger
            );

            await propertyService.UpdateProperty(
                expectedUpdated.Id,
                new PropertyDto
                {
                    Address = "Fake ST 3",
                    CodeInternal = "003",
                    Id = 3,
                    Name = "Property 3",
                    Price = 3.333f,
                    Year = 3
                }
            );

            var propertyUpdated = propertyService.GetById(expectedUpdated.Id);

            Assert.IsNotNull(propertyUpdated);
            Assert.AreEqual(expectedUpdated.Name, propertyUpdated.Name);
            Assert.AreEqual(expectedUpdated.Price, propertyUpdated.Price);
            Assert.AreEqual(expectedUpdated.CodeInternal, propertyUpdated.CodeInternal);
            Assert.AreEqual(expectedUpdated.Address, propertyUpdated.Address);
        }

    }
}
