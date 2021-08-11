using Moq;
using Xunit;
using DataLayer.Abstraction.Repositories;
using BusinesLogic.Services;
using System;

namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async void GetUserByLogonAsyncFromAuthentificate_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var service = new UserService(mockRepo.Object);
            // Act
            await service.Authentificate("l","p");
            // Assert 
            mockRepo.Verify(mr => mr.GetUserByLogonAsync(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetUserByLogonAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var service = new UserService(mockRepo.Object);
            // Act
            await service.GetUserByLogonAsync("l", "p");
            // Assert 
            mockRepo.Verify(mr => mr.GetUserByLogonAsync(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async void CreateNewUserAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var service = new UserService(mockRepo.Object);
            // Act
            await service.CreateNewUserAsync("l", "p");
            // Assert 
            mockRepo.Verify(mr => mr.CreateNewUserAsync(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async void GetUserByLoginAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var service = new UserService(mockRepo.Object);
            // Act
            await service.GetUserByLoginAsync("l");
            // Assert 
            mockRepo.Verify(mr => mr.GetUserByLoginAsync(It.IsAny<string>()));
        }

        [Fact]
        public void GenerateRefreshToken_IsGenerated_True()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var service = new UserService(mockRepo.Object);
            // Act
            var result = service.GenerateRefreshToken(1);
            // Assert 
            Assert.NotNull(result.Token);
            Assert.InRange(result.Expires, DateTime.Now, DateTime.Now.AddMinutes(61));
        }
    }
}
