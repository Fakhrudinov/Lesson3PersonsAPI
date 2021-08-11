using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.DTO;

namespace Tests
{
    public class PersonIDValidationServiceTests
    {
        private PersonIDValidationService _validator = new PersonIDValidationService();

        [Fact]
        public void PersonId_Correct()
        {
            // Arrange
            var model = new PersonToGet { Id = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Id);
        }

        [Fact]
        public void PersonId_is0_MustBeError()
        {
            // Arrange
            var model = new PersonToGet { Id = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Id);
            Assert.Equal("P-100.1", result.Errors[0].ErrorCode);
        }
    }
}
