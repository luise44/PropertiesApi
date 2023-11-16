using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Properties.Data.Entities;
using Properties.Data.Repositories.Interfaces;
using Properties.Services.Application.Automapper;
using Properties.Services.Application.Services;
using Properties.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Properties.Test.ServicesTests
{
    [TestClass]
    public class UserServiceTest
    {
        private ILogger<UserService> logger;
        private IMapper mapper;

        [TestInitialize]
        public void Setup()
        {
            var mapperProfile = new ServicesProfile();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            mapper = new Mapper(mapperConfiguration);

            logger = new Mock<ILogger<UserService>>().Object;
        }

        [TestMethod]
        public async Task Created_User_Not_Null()
        {
            var newUser = new UserDto
            {
                Id = 3,
                Email = "luis333@email.com",
                Password = "123"
            };

            var userRepository = new Mock<IUserRepository>();
            userRepository
                .Setup(x => x.Find(It.IsAny<Func<User, bool>>()))
                .Returns(new List<User>());

            userRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new User
                {
                    Id = 3,
                    Email = "luis333@email.com",
                    Password = "123"
                });

            var userService = new UserService(
                userRepository.Object,
                mapper,
                logger
            );

            await userService.CreateUser(newUser);

            var createdUser = userService.GetUserById(newUser.Id);

            Assert.AreEqual(newUser.Id, createdUser.Id);
            Assert.AreEqual(newUser.Email, createdUser.Email);
            Assert.AreEqual(newUser.Password, createdUser.Password);
        }

        [TestMethod]
        public void Get_User_By_Id_Not_Null()
        {
            var userExpected = new UserDto
            {
                Id = 3,
                Email = "luis333@email.com",
                Password = "123"
            };

            var userRepository = new Mock<IUserRepository>();

            userRepository
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(mapper.Map<User>(userExpected));

            var userService = new UserService(
                userRepository.Object,
                mapper,
                logger
            );

            var foundUser = userService.GetUserById(userExpected.Id);

            Assert.IsNotNull(foundUser);
            Assert.AreEqual(userExpected.Id, foundUser.Id);
            Assert.AreEqual(userExpected.Email, foundUser.Email);
            Assert.AreEqual(userExpected.Password, foundUser.Password);
        }

        [TestMethod]
        public void Get_User_By_Email_Not_Null()
        {
            var userExpected = new UserDto
            {
                Id = 3,
                Email = "luis333@email.com",
                Password = "123"
            };

            var userRepository = new Mock<IUserRepository>();

            userRepository
                .Setup(x => x.Find(It.IsAny<Func<User, bool>>()))
                .Returns(new List<User>()
                {
                    mapper.Map<User>(userExpected)
                });

            var userService = new UserService(
                userRepository.Object,
                mapper,
                logger
            );

            var foundUser = userService.GetUserByEmail(userExpected.Email);

            Assert.IsNotNull(foundUser);
            Assert.AreEqual(userExpected.Id, foundUser.Id);
            Assert.AreEqual(userExpected.Email, foundUser.Email);
            Assert.AreEqual(userExpected.Password, foundUser.Password);
        }

    }
}
