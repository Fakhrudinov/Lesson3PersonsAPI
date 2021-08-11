using Xunit;
using BusinesLogic.Abstraction.Requests;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;

namespace Tests
{
    public class ClinicValidationServiceTests
    {
        private ClinicValidationService _validator = new ClinicValidationService();

        [Fact]
        public void ClinicName_Correct()
        {
            // Arrange
            var model = new ClinicToPost { Name = "abc" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Name);
        }

        [Fact]
        public void ClinicAdress_Correct()
        {
            // Arrange
            var model = new ClinicToPost { Adress = "abcde" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Adress);
        }

        [Fact]
        public void ClinicName_TooShort_MustBeError()
        {
            // Arrange
            var model = new ClinicToPost { Name = "a" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Name);
            Assert.Equal("C-100.11", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ClinicAdress_TooShort_MustBeError()
        {
            // Arrange
            var model = new ClinicToPost { Adress = "a" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Adress);
            Assert.Equal("C-100.12", result.Errors[0].ErrorCode);
        }
    }
}
