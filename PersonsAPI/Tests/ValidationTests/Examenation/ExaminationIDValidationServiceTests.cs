using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.DTO;

namespace Tests
{
    public class ExaminationIDValidationServiceTests
    {
        private ExaminationIDValidationService _validator = new ExaminationIDValidationService();

        [Fact]
        public void ExaminationId_Correct()
        {
            // Arrange
            var model = new ExaminationToGet { Id = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Id);
        }

        [Fact]
        public void ExaminationId_is0_MustBeError()
        {
            // Arrange
            var model = new ExaminationToGet { Id = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Id);
            Assert.Equal("E-100.7", result.Errors[0].ErrorCode);
        }
    }
}
