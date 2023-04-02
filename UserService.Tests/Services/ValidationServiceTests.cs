using Moq;
using Moq.EntityFrameworkCore;
using UserService.Database;
using UserService.Interfaces.Models;
using UserService.Services;

namespace UserService.Tests.Services
{
    [TestClass]
    public class ValidationServiceTests
    {
        private ValidationService validationService;
        private DateTime createdDate = DateTime.Now.Date;

        [TestInitialize]
        public void Setup()
        {
            var dbContextMock = new Mock<UserDbContext>();

            dbContextMock.Setup(x => x.UserData).ReturnsDbSet(new List<UserDataModel>()
            {
                new UserDataModel() { Id = 6, Name = "name", CreatedDate = createdDate, UserId = 1, User = new UserModel { Id = 1 } },
                new UserDataModel() { Id = 6, Name = "name", CreatedDate = createdDate, UserId = 1, User = new UserModel { Id = 1 } },
                new UserDataModel() { Id = 6, Name = "name", CreatedDate = createdDate, UserId = 1, User = new UserModel { Id = 1 } },
                new UserDataModel() { Id = 6, Name = "name", CreatedDate = createdDate, UserId = 1, User = new UserModel { Id = 1 } },
                new UserDataModel() { Id = 6, Name = "name", CreatedDate = createdDate, UserId = 1, User = new UserModel { Id = 1 } }
            });

            validationService = new ValidationService(dbContextMock.Object);
        }

        [TestMethod]
        public async Task IsUserDataValidAsync_Test01()
        {
            var userDataModel = new UserDataModel() { Id = 6, Name = "name", CreatedDate = createdDate, UserId = 1, User = new UserModel { Id = 1 } };

            var result = await validationService.IsUserDataValidAsync(userDataModel);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task IsUserDataValidAsync_Test02()
        {
            var userDataModel = new UserDataModel() { Id = 6, Name = "name", CreatedDate = createdDate, UserId = 2, User = new UserModel { Id = 2 } };

            var result = await validationService.IsUserDataValidAsync(userDataModel);

            Assert.IsTrue(result);
        }
    }
}