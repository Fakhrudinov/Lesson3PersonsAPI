using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.Requests;

namespace Tests
{
    public class ClinicSearchWithPaginationValidationServiceTests
    {
        private ClinicSearchWithPaginationValidationService _validator = new ClinicSearchWithPaginationValidationService();

        [Fact]
        public void ClinicSearchTerm_Correct()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { SearchTerm = "ab" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.SearchTerm);
        }

        [Fact]
        public void ClinicSearchTerm_isEmpty_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { SearchTerm = "" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.SearchTerm);
            Assert.Equal("C-100.5", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ClinicSearchTerm_TooLong_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { SearchTerm = "12345678901" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.SearchTerm);
            Assert.Equal("C-100.5", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ClinicSkip_Correct()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Skip = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Skip);
        }

        [Fact]
        public void ClinicSkip_LessThan0_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Skip = -1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Skip);
            Assert.Equal("C-100.6", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ClinicSkip_GreaterThan50_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Skip = 51 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Skip);
            Assert.Equal("C-100.7", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ClinicTake_Correct()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Take = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Take);
        }

        [Fact]
        public void ClinicTake_LessThan1_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Take = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Take);
            Assert.Equal("C-100.8", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ClinicTake_GreaterThan100_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Take = 101 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Take);
            Assert.Equal("C-100.9", result.Errors[0].ErrorCode);
        }
    }
}
