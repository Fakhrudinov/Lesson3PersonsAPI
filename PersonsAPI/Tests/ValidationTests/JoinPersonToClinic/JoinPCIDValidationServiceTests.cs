using Xunit;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;

namespace Tests
{
    public class JoinPCIDValidationServiceTests
    {
        private JoinPCIDValidationService _validator = new JoinPCIDValidationService();

        [Fact]
        public void JoinPCID_ClinicId_Correct()
        {
            // Arrange
            var model = new IdIdRequest { ClinicId = 1, PersonId = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.ClinicId);
        }

        [Fact]
        public void JoinPCID_ClinicId_is0_MustBeError()
        {
            // Arrange
            var model = new IdIdRequest { ClinicId = 0, PersonId = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.ClinicId);
            Assert.Equal("J-100.1", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void JoinPCID_PersonId_Correct()
        {
            // Arrange
            var model = new IdIdRequest { ClinicId = 1, PersonId = 1 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.PersonId);
        }

        [Fact]
        public void JoinPCID_PersonId_is0_MustBeError()
        {
            // Arrange
            var model = new IdIdRequest { ClinicId = 1, PersonId = 0 };
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.PersonId);
            Assert.Equal("J-100.2", result.Errors[0].ErrorCode);
        }
    }
}
