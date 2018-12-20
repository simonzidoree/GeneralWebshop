using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Moq;
using Webshop.Core.ApplicationService;
using Webshop.Core.ApplicationService.Services;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;
using Xunit;

namespace Webshop.xUnitTests.Core.ApplicationService.Services
{
    public class UserServiceTest
    {
        private Mock<IUserRepository> CreateUserMockRepository()
        {
            var password = "b";
            byte[] passwordHashA, passwordSaltA, passwordHashAA, passwordSaltAA;
            CreatePasswordHash(password, out passwordHashA, out passwordSaltA);
            CreatePasswordHash(password, out passwordHashAA, out passwordSaltAA);

            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Username = "a",
                    PasswordHash = passwordHashA,
                    PasswordSalt = passwordSaltA,
                    IsAdmin = false
                },
                new User
                {
                    Id = 2,
                    Username = "aa",
                    PasswordHash = passwordHashAA,
                    PasswordSalt = passwordSaltAA,
                    IsAdmin = true
                }
            };

            var repository = new Mock<IUserRepository>();
            repository.Setup(r => r.GetAll())
                .Returns(users);
            repository.Setup(x => x.GetById(1)).Returns(users[0]);

            return repository;
        }

        [Theory]
        [InlineData(1, 1)]
        private void GetUserById(int id, int expected)
        {
            var repository = CreateUserMockRepository();
            IUserService service = new UserService(repository.Object);

            var actual = service.GetById(id).Id;

            Assert.Equal(expected, actual);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        [Fact]
        public void GetAllUserCountThrowsNoException()
        {
            var mockUserRepo = CreateUserMockRepository();
            IUserService service = new UserService(mockUserRepo.Object);

            var expectedUsersCount = 2;
            var actualUsersCount = service.GetAll().Count();

            Assert.Equal(expectedUsersCount, actualUsersCount);
        }

        [Fact]
        public void VerifyCreateUserCallUserRepoCreateUserOnce()
        {
            var password = "b";
            byte[] passwordHashA, passwordSaltA;
            CreatePasswordHash(password, out passwordHashA, out passwordSaltA);
            var mockUserRepo = new Mock<IUserRepository>();
            IUserService service = new UserService(mockUserRepo.Object);

            var user = new User
            {
                Id = 1,
                Username = "a",
                PasswordHash = passwordHashA,
                PasswordSalt = passwordSaltA,
                IsAdmin = false
            };

            service.Create(user);
            mockUserRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }
    }
}