using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.Requests;

namespace Tests
{
    public class PersonSearchValidationServiceTests
    {
        private PersonSearchValidationService _validator = new PersonSearchValidationService();

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
            Assert.Equal("P-100.2", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void PersonSearchTerm_isNull_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { SearchTerm = null };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.SearchTerm);
            Assert.Equal("P-100.2", result.Errors[0].ErrorCode);
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
            Assert.Equal("P-100.4", result.Errors[0].ErrorCode);
        }
    }
}
