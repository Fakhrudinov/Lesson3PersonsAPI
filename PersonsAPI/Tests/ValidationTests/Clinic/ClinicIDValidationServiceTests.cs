using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.DTO;

namespace Tests
{
    public class ClinicIDValidationServiceTests
    {
        private ClinicIDValidationService _validator = new ClinicIDValidationService();

        [Fact]
        public void ClinicId_Correct()
        {
            // Arrange
            var model = new ClinicToGet { Id = 1};
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Id);
        }

        [Fact]
        public void ClinicId_is0_MustBeError()
        {
            // Arrange
            var model = new ClinicToGet { Id = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Id);
            Assert.Equal("C-100.1", result.Errors[0].ErrorCode);
        }
    }
}
