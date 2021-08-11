using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.DTO;

namespace Tests
{
    public class JoinIDValidationServiceTests
    {
        private JoinIDValidationService _validator = new JoinIDValidationService();

        [Fact]
        public void JoinId_Correct()
        {
            // Arrange
            var model = new PersonToGet { Id = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Id);
        }

        [Fact]
        public void JoinId_is0_MustBeError()
        {
            // Arrange
            var model = new PersonToGet { Id = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Id);
            Assert.Equal("J-100.3", result.Errors[0].ErrorCode);
        }
    }
}
