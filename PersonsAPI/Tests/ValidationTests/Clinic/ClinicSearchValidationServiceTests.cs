using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.Requests;

namespace Tests
{
    public class ClinicSearchValidationServiceTests
    {
        private ClinicSearchValidationService _validator = new ClinicSearchValidationService();

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
            Assert.Equal("C-100.2", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ClinicSearchTerm_isNull_MustBeError()
        {
            // Arrange
            var model = new SearchWithPaginationRequest { SearchTerm = null };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.SearchTerm);
            Assert.Equal("C-100.2", result.Errors[0].ErrorCode);
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
            Assert.Equal("C-100.4", result.Errors[0].ErrorCode);
        }
    }
}
