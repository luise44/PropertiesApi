using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using Properties.Data;
using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;
using Properties.Data.Repositories.Repositories;
using Properties.Services.Application.Automapper;
using Properties.Services.Application.Interfaces;
using Properties.Services.Application.Services;
using Properties.Services.DTO;
using Properties.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Test.ServicesTests
{
    [TestClass]
    public class OwnerServiceTest
    {
        private List<Owner> owners;
        private IMapper mapper;
        private ILogger<OwnerService> logger;

        [TestInitialize]
        public void Setup()
        {
            owners = new List<Owner>
            {
                new Owner
                {
                    Id = 1,
                    Address = "Fake ST 1",
                    Birthday = DateTime.Now,
                    Name = "Onwer 1",
                    Photo = new byte[] {}
                },
                new Owner
                {
                    Id = 2,
                    Address = "Fake ST 2",
                    Birthday = DateTime.Now,
                    Name = "Onwer 2",
                    Photo = new byte[] {}
                }
            };

            var mapperProfile = new ServicesProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            mapper = new Mapper(mapperConfiguration);

            logger = new Mock<ILogger<OwnerService>>().Object;
        }

        [TestMethod]
        public void Get_All_Not_Empty()
        {
            var expectedCount = 2;

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository.Setup(x => x.GetAll()).Returns(owners);

            var ownerService = new OwnerService(
                ownerRepository.Object,
                mapper,
                logger
            );

            var ownersFromService = ownerService.GetAll();

            Assert.AreEqual(expectedCount, ownersFromService.Count);
        }

        [TestMethod]
        public async Task Get_Created_Owner_Not_Null()
        {
            var newOwner = new OwnerDto
            {
                Address = "Fake ST 3",
                Birthday= DateTime.Now,
                Id = 3,
                Name = "Owner 3",
                Photo = new byte[] {}
            };

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository
                .Setup(x=> x.GetById(It.IsAny<int>()))
                .Returns(new Owner
                {
                    Address = "Fake ST 3",
                    Birthday = DateTime.Now,
                    Id = 3,
                    Name = "Owner 3",
                    Photo = new byte[] { }
                });

            var ownerService = new OwnerService(
                ownerRepository.Object,
                mapper,
                logger
            );

            await ownerService.CreateOwner(newOwner);

            var ownerCreated = ownerService.GetOwnerById(newOwner.Id);

            Assert.IsNotNull(ownerCreated);
        }

        [TestMethod]
        public void Get_Owner_By_Id_Not_Null()
        {
            var ownerId = 3;

            var ownerRepository = new Mock<IOwnerRepository>();
            ownerRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Owner
                {
                    Address = "Fake ST 3",
                    Birthday = DateTime.Now,
                    Id = 3,
                    Name = "Owner 3",
                    Photo = new byte[] { }
                });

            var ownerService = new OwnerService(
                ownerRepository.Object,
                mapper,
                logger
            );

            var owner = ownerService.GetOwnerById(ownerId);

            Assert.IsNotNull(owner);
        }
    }
}
