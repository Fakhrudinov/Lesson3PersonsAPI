using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using PersonsAPI.Requests;

namespace Tests
{
    public class PersonValidationServiceTests
    {
        private PersonValidationService _validator = new PersonValidationService();

        [Fact]
        public void PersonFirstName_Correct()
        {
            // Arrange
            var model = new PersonToPost { FirstName = "Васиссуалий" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.FirstName);
        }

        [Fact]
        public void PersonFirstName_TooShort_MustBeError()
        {
            // Arrange
            var model = new PersonToPost { FirstName = "a" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.FirstName);
            Assert.Equal("P-100.9", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonFirstName_NotValid_MustBeError()
        {
            // Arrange
            var model = new PersonToPost { FirstName = "a23e8o7cn" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.FirstName);
            Assert.Equal("P-100.11", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonLastName_Correct()
        {
            // Arrange
            var model = new PersonToPost { LastName = "Пупкин" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.LastName);
        }

        [Fact]
        public void PersonLastName_TooShort_MustBeError()
        {
            // Arrange
            var model = new PersonToPost { LastName = "a" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.LastName);
            Assert.Equal("P-100.9", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonLastName_NotValid_MustBeError()
        {
            // Arrange
            var model = new PersonToPost { LastName = "w3o4ci8j7nha" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.LastName);
            Assert.Equal("P-100.12", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonEmail_Correct()
        {
            // Arrange
            var model = new PersonToPost { Email = "qwe@asd.zx" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Email);
        }

        [Fact]
        public void PersonEmail_TooShort_MustBeError()
        {
            // Arrange
            var model = new PersonToPost { Email = "a" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Email);
            Assert.Equal("P-100.10", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonEmail_NotValid_MustBeError()
        {
            // Arrange
            var model = new PersonToPost { Email = "8i3w47@h6cd5" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Email);
            Assert.Equal("P-100.13", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonAge_Correct()
        {
            // Arrange
            var model = new PersonToPost { Age = 5 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Age);
        }

        [Fact]
        public void PersonAge_is0_MustBeError()
        {
            // Arrange
            var model = new PersonToPost { Age = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Age);
            Assert.Equal("P-100.14", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonAge_isGreaterThen120_MustBeError()
        {
            // Arrange
            var model = new PersonToPost { Age = 121 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Age);
            Assert.Equal("P-100.15", result.Errors[0].ErrorCode);
        }
    }
}
