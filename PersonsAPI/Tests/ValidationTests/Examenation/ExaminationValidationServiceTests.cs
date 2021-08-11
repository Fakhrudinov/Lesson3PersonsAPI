using Xunit;
using BusinesLogic.Abstraction.Requests;
using FluentValidation.TestHelper;
using BusinesLogic.ValidateControllerData;
using System;

namespace Tests
{
    public class ExaminationValidationServiceTests
    {
        private ExaminationValidationService _validator = new ExaminationValidationService();
        ExaminationToPost model = new ExaminationToPost("procedure x", DateTime.Now, 100, 2, 2);        

        [Fact]
        public void ExaminationProcedureName_Correct()
        {
            // Arrange
            
            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.ProcedureName);
        }

        [Fact]
        public void ExaminationProcedureDate_Correct()
        {
            // Arrange

            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.ProcedureDate);
        }

        [Fact]
        public void ExaminationProcedureCost_Correct()
        {
            // Arrange

            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.ProcedureCost);
        }
        [Fact]
        public void ExaminationClinicId_Correct()
        {
            // Arrange

            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.ClinicId);
        }

        [Fact]
        public void ExaminationPersonId_Correct()
        {
            // Arrange

            // Act
            var result = _validator.TestValidate(model);
            // Assert 
            result.ShouldNotHaveValidationErrorFor(cl => cl.PersonId);
        }

        [Fact]
        public void ExaminationProcedureName_TooShort_MustBeError()
        {
            // Arrange
            var badmodel = new ExaminationToPost("x", DateTime.MinValue, 0, 0, 0);
            // Act
            var result = _validator.TestValidate(badmodel);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.ProcedureName);
            Assert.Equal("E-100.01", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ExaminationProcedureName_TooLong_MustBeError()
        {
            // Arrange
            var badmodel = new ExaminationToPost("lkunhiluwervhnlweoirtvn;iowerbnv;toierjbtoiewrtbj;noiewrjntb;oiwerj;tbowierj;tboiwejrt;boiewrjt;bnoierjtbn;oiewrjtbpnoeiwrtjnb;oiewrjntb;eoirj;iturveriouv", DateTime.MinValue, 0, 0, 0);
            // Act
            var result = _validator.TestValidate(badmodel);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.ProcedureName);
            Assert.Equal("E-100.01", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ExaminationProcedureDate_isMinValue_MustBeError()
        {
            // Arrange
            var badmodel = new ExaminationToPost("qwerty", DateTime.MinValue, 2, 2, 2);
            // Act
            var result = _validator.TestValidate(badmodel);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.ProcedureDate);
            Assert.Equal("E-100.02", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ExaminationProcedureCost_is0_MustBeError()
        {
            // Arrange
            var badmodel = new ExaminationToPost("qwerty", DateTime.Now, 0, 2, 2);
            // Act
            var result = _validator.TestValidate(badmodel);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.ProcedureCost);
            Assert.Equal("E-100.4", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ExaminationPersonId_is0_MustBeError()
        {
            // Arrange
            var badmodel = new ExaminationToPost("qwerty", DateTime.Now, 2, 0, 2);
            // Act
            var result = _validator.TestValidate(badmodel);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.PersonId);
            Assert.Equal("E-100.6", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void ExaminationClinicId_is0_MustBeError()
        {
            // Arrange
            var badmodel = new ExaminationToPost("qwerty", DateTime.Now, 2, 2, 0);
            // Act
            var result = _validator.TestValidate(badmodel);
            // Assert 
            result.ShouldHaveValidationErrorFor(cl => cl.ClinicId);
            Assert.Equal("E-100.5", result.Errors[0].ErrorCode);
        }
    }
}
