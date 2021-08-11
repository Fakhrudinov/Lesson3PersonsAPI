using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.Requests;

namespace Tests
{
    public class JoinhWithPaginationValidationServiceTests
    {
        private JoinWithPaginationValidationService _validator = new JoinWithPaginationValidationService();

        [Fact]
        public void JoinWithPaginationSkip_Correct()
        {
            // Arrange
            var model = new IdWithPaginationRequest { Skip = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Skip);
        }

        [Fact]
        public void JoinWithPaginationSkip_LessThan0_MustBeError()
        {
            // Arrange
            var model = new IdWithPaginationRequest { Skip = -1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Skip);
            Assert.Equal("J-100.4", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void JoinWithPaginationSkip_GreaterThan50_MustBeError()
        {
            // Arrange
            var model = new IdWithPaginationRequest { Skip = 51 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Skip);
            Assert.Equal("J-100.5", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void JoinWithPaginationTake_Correct()
        {
            // Arrange
            var model = new IdWithPaginationRequest { Take = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Take);
        }

        [Fact]
        public void JoinWithPaginationTake_LessThan1_MustBeError()
        {
            // Arrange
            var model = new IdWithPaginationRequest { Take = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Take);
            Assert.Equal("J-100.6", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void JoinWithPaginationTake_GreaterThan100_MustBeError()
        {
            // Arrange
            var model = new IdWithPaginationRequest { Take = 101 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Take);
            Assert.Equal("J-100.7", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void JoinWithPaginationId_Correct()
        {
            // Arrange
            var model = new IdWithPaginationRequest { Id = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Id);
        }

        [Fact]
        public void JoinWithPaginationId_is0_MustBeError()
        {
            // Arrange
            var model = new IdWithPaginationRequest { Id = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Id);
            Assert.Equal("J-100.6", result.Errors[0].ErrorCode);
        }
    }
}
