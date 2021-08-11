using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.Requests;

namespace Tests
{
    public class PersonSearchWithPaginationValidationServiceTests
    {
        private PersonSearchWithPaginationValidationService _validator = new PersonSearchWithPaginationValidationService();

        [Fact]
        public void PersonSearchTerm_Correct()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { SearchTerm = "ab" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.SearchTerm);
        }

        [Fact]
        public void PersonSearchTerm_isEmpty_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { SearchTerm = "" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.SearchTerm);
            Assert.Equal("P-100.5", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonSearchTerm_TooLong_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { SearchTerm = "12345678901" };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.SearchTerm);
            Assert.Equal("P-100.5", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonSkip_Correct()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Skip = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Skip);
        }

        [Fact]
        public void PersonSkip_LessThan0_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Skip = -1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Skip);
            Assert.Equal("P-100.6", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonSkip_GreaterThan50_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Skip = 51 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Skip);
            Assert.Equal("P-100.7", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonTake_Correct()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Take = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.Take);
        }

        [Fact]
        public void PersonTake_LessThan1_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Take = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Take);
            Assert.Equal("P-100.8", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonTake_GreaterThan100_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { Take = 101 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.Take);
            Assert.Equal("P-100.9", result.Errors[0].ErrorCode);
        }
    }
}
