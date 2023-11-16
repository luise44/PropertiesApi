using Properties.Data;
using Properties.Data.Entities;
using Properties.Data.Repositories.Repositories;
using Properties.Test.Helpers;

namespace Properties.Test.RepositoriesTests
{
    [TestClass]
    public class UserRepositoryTest
    {

        private List<User> users;
        private PropertiesDbContext dbContext;

        [TestInitialize]
        public void SetUp()
        {
            users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "luis11@email.com",
                    Password = "123"
                },
                new User
                {
                    Id = 2,
                    Email = "luis22@email.com",
                    Password = "456"
                }
            };

            dbContext = DataHelper.GetDbContext<User>(users, x => x.Set<User>());
        }

        [TestMethod]
        public void GetById_Returns_NotNull()
        {
            var userRepository = new UserRepository(dbContext);

            var searchId = 1;

            var result = userRepository.GetById(searchId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetById_Expected_Result_AreEqual()
        {
            User expectedUser = new User
            {
                Email = "luis11@email.com",
                Id = 1,
                Password = "123"
            };

            var userRepository = new UserRepository(dbContext);

            var searchId = 1;

            var result = userRepository.GetById(searchId);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedUser.Id, result.Id);
            Assert.AreEqual(expectedUser.Email, result.Email);
            Assert.AreEqual(expectedUser.Password, result.Password);
        }

        [TestMethod]
        public void Find_Expected_Returns_NotEmpty()
        {
            var userRepository = new UserRepository(dbContext);

            var result = userRepository.Find(x => x.Email.Contains("luis")).ToList();

            var expectedCount = 2;

            Assert.AreEqual(expectedCount, result.Count);
        }

        [TestMethod]
        public void GetAll_Expected_Returns_NotEmpty()
        {
            var userRepository = new UserRepository(dbContext);

            var result = userRepository.GetAll();

            var expectedCount = 2;

            Assert.AreEqual(expectedCount, result.Count);
        }

        [TestMethod]
        public void Added_User_Is_Valid()
        {
            var newUser = new User
            {
                Email = "luise33@email.com",
                Password = "123",
                Id = 3
            };

            var userRepository = new UserRepository(dbContext);

            userRepository.Add(newUser);

            var result = userRepository.GetById(3);

            Assert.IsNotNull(result);
            Assert.AreEqual(newUser.Id, result.Id);
            Assert.AreEqual(newUser.Email, result.Email);
            Assert.AreEqual(newUser.Password, result.Password);
        }

        [TestMethod]
        public void Updated_User_Is_Changed()
        {
            var userIdToUpdate = 2;
            var emailToUpdate = "luis44@email.com";

            var userRepository = new UserRepository(dbContext);

            var userToUpdate = userRepository.GetById(userIdToUpdate);

            Assert.IsNotNull(userToUpdate);

            userToUpdate.Email = emailToUpdate;

            userRepository.Update(userToUpdate);

            var userUpdated = userRepository.GetById(userIdToUpdate);

            Assert.AreEqual(userUpdated.Email, emailToUpdate);
        }

        [TestMethod]
        public void Deleted_User_Is_Null()
        {
            var userIdToDelete = 2;

            var userRepository = new UserRepository(dbContext);

            var userToDelete = userRepository.GetById(userIdToDelete);

            Assert.IsNotNull(userToDelete);

            userRepository.Remove(userToDelete);

            var result = userRepository.GetById(userIdToDelete);

            Assert.IsNull(result);
        }
    }
}