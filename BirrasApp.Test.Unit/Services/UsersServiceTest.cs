using AutoMapper;
using DTO = BirrasApp.DTOs;
using Logic = BirrasApp.Services.Models;
using Repo = BirrasApp.Repositories.Models;
using BirrasApp.Mappers;
using BirrasApp.Repositories.Interfaces;
using BirrasApp.Services;
using BirrasApp.Services.Interfaces;
using Moq;
using System;

using System.Threading.Tasks;
using Xunit;

namespace BirrasApp.Test.Unit.Services
{
    public class UsersServiceTest
    {
        private readonly Mock<IUsersRepository> _userRepositoryMock;
        private readonly Mock<ISecurityService> _securityServiceMock;
        public UsersServiceTest()
        {
            _userRepositoryMock = new Mock<IUsersRepository>();
            _securityServiceMock = new Mock<ISecurityService>();
        }

        [Fact]
        public async Task Login_WhenCorrectLogin_Should_ReturnOk()
        {
            // Arrange
            var userLogin = new Logic.User()
            {
                Username = "TestUserName",
                Password = "TestPassword"
            };

            var userRepo = new Repo.User()
            {
                Username = "TestUserName"
            };

            _userRepositoryMock.Setup(ur => ur.GetByUsername(It.IsAny<String>())).ReturnsAsync(userRepo);
            _securityServiceMock.Setup(ss => ss.VerifyPasswordHash(
                    It.IsAny<String>(), 
                    It.IsAny<byte[]>(), 
                    It.IsAny<byte[]>()
                )).Returns(true);

            var service = GetUserService();

            // Act
            var result = await service.Login(userLogin);

            // Assert
            Assert.NotNull(result);
        }

        private UserService GetUserService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelsProfile());
            });
            var mapper = config.CreateMapper();
            return new UserService(_userRepositoryMock.Object, mapper, _securityServiceMock.Object);
        }
    }
}
