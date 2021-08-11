using Xunit;
using BusinesLogic.Abstraction.Requests;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using PersonsAPI.Requests;

namespace Tests
{
    public class UserValidationServiceTests
    {
        private UserValidationService _validator = new UserValidationService();

        [Fact]
        public void UserLogin_Correct()
        {
            // Arrange
            var model = new UserToPost { Login = "abcd", Password = "abcd1@QW" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Login);
        }

        [Fact]
        public void UserPassword_Correct()
        {
            // Arrange
            var model = new UserToPost { Login = "abcd", Password = "abcd1@QW" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Password);
        }

        [Fact]
        public void UserLogin_IsEmpty_MustBeError()
        {
            // Arrange
            var model = new UserToPost { Login = "", Password = "abcd1@QW" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Login);
            Assert.Equal("L-100.1", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void UserLogin_TooShort_MustBeError()
        {
            // Arrange
            var model = new UserToPost { Login = "a", Password = "abcd1@QW" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Login);
            Assert.Equal("L-100.2", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void UserPassword_IsEmpty_MustBeError()
        {
            // Arrange
            var model = new UserToPost { Login = "abcd", Password = "" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Password);
            Assert.Equal("L-200.1", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void UserPassword_TooShort_MustBeError()
        {
            // Arrange
            var model = new UserToPost { Login = "abcd", Password = "a" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Password);
            Assert.Equal("L-200.2", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void UserPassword_NotValid_MustBeError()
        {
            // Arrange
            var model = new UserToPost { Login = "abcd", Password = "abcdefgh" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Password);
            Assert.Equal("L-200.3", result.Errors[0].ErrorCode);
        }
    }
}
