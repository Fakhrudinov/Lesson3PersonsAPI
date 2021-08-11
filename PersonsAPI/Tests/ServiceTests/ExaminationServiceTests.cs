using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Abstraction.Repositories;
using DataLayer.Abstraction.Entityes;
using System.Threading.Tasks;
using BusinesLogic.Services;
using BusinesLogic.Abstraction.Requests;
using BusinesLogic.Abstraction.DTO;
using System;
using BusinesLogic.Abstraction.Services;
using DataLayer.Repositories;

namespace Tests
{
    public class ExaminationServiceTests
    {
        private IEnumerable<ExaminationDataLayer> ExaminationsList()
        {
            IList<ExaminationDataLayer> clinics = new List<ExaminationDataLayer>
                {
                    new ExaminationDataLayer { Id = 1, ProcedureName = "procedure 1", ClinicId = 1 , PersonId = 1, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(0), IsPaid = true, PaidDate = DateTime.Now },
                    new ExaminationDataLayer { Id = 2, ProcedureName = "procedure 2", ClinicId = 1 , PersonId = 2, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(1), IsPaid = false },
                    new ExaminationDataLayer { Id = 3, ProcedureName = "procedure 3", ClinicId = 1 , PersonId = 3, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(2), IsPaid = true, PaidDate = DateTime.Now },
                    new ExaminationDataLayer { Id = 4, ProcedureName = "procedure 4", ClinicId = 1 , PersonId = 4, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(2), IsPaid = false },
                    new ExaminationDataLayer { Id = 5, ProcedureName = "procedure 5", ClinicId = 4 , PersonId = 4, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(0), IsPaid = true, PaidDate = DateTime.Now },
                    new ExaminationDataLayer { Id = 6, ProcedureName = "procedure 6", ClinicId = 4 , PersonId = 5, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(1), IsPaid = false },
                    new ExaminationDataLayer { Id = 7, ProcedureName = "procedure 7", ClinicId = 4 , PersonId = 1, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(2), IsPaid = true, PaidDate = DateTime.Now },
                    new ExaminationDataLayer { Id = 8, ProcedureName = "procedure 8", ClinicId = 4 , PersonId = 7, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(3), IsPaid = false },
                    new ExaminationDataLayer { Id = 9, ProcedureName = "procedure 9",   ClinicId = 2 , PersonId = 2, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(0), IsPaid = true, PaidDate = DateTime.Now },
                    new ExaminationDataLayer { Id = 10, ProcedureName = "procedure 10", ClinicId = 2 , PersonId = 1, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(1), IsPaid = false },
                    new ExaminationDataLayer { Id = 11, ProcedureName = "procedure 11", ClinicId = 2 , PersonId = 8, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(32), IsPaid = true, PaidDate = DateTime.Now },
                    new ExaminationDataLayer { Id = 12, ProcedureName = "procedure 12", ClinicId = 2 , PersonId = 6, ProcedureCost = 100, ProcedureDate = DateTime.Now.AddHours(33), IsPaid = false }
                };
            return clinics;
        }

        [Fact]
        public async Task GetExaminationsAsync_Return12Examinations_True()
        {
            // Arrange
            var mockRepo = new Mock<IExaminationRepository>();
            var service = new ExaminationService(mockRepo.Object);
            mockRepo
                .Setup(repo => repo.GetExaminationsAsync())
                .ReturnsAsync(ExaminationsList());
            // Act
            var result = await service.GetAllExaminationsAsync();
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(12, result.Count());
        }


        [Fact]
        public async void GetExaminationByIdAsync_Return1Examination_True()
        {
            // Arrange
            var mockRepo = new Mock<IExaminationRepository>();
            var service = new ExaminationService(mockRepo.Object);
            int searchId = 2;
            mockRepo
                .Setup(mr => mr.GetExaminationByIdAsync(searchId))
                .ReturnsAsync(ExaminationsList()
                .Where(x => x.Id == searchId)
                .FirstOrDefault);
            // Act
            var result = await service.GetExaminationByIdAsync(searchId);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal("procedure 2", result.ProcedureName);
        }

        [Fact]
        public async void GetExaminationsByClientIdAsync_Return2Examination_True()
        {
            // Arrange
            var mockRepo = new Mock<IExaminationRepository>();
            var service = new ExaminationService(mockRepo.Object);
            int searchId = 1;
            mockRepo
                .Setup(mr => mr.GetExaminationsByClientIdAsync(searchId, It.IsAny<bool>()))
                .ReturnsAsync(ExaminationsList()
                .Where(x => x.PersonId == searchId && x.IsPaid == true));
            // Act
            var result = await service.GetExaminationsByClientIdAsync(searchId, true);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetExaminationsByClinicIdAsync_Return2Examination_True()
        {
            // Arrange
            var mockRepo = new Mock<IExaminationRepository>();
            var service = new ExaminationService(mockRepo.Object);
            int searchId = 1;
            mockRepo
                .Setup(mr => mr.GetExaminationsByClinicIdAsync(searchId, It.IsAny<bool>()))
                .ReturnsAsync(ExaminationsList()
                .Where(x => x.ClinicId == searchId && x.IsPaid == true));
            // Act
            var result = await service.GetExaminationsByClinicIdAsync(searchId, true);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetExaminationsFromDateByClinicIdAsync_Return2Examination_True()
        {
            // Arrange
            var mockRepo = new Mock<IExaminationRepository>();
            var service = new ExaminationService(mockRepo.Object);
            int searchId = 1;
            mockRepo
                .Setup(mr => mr.GetExaminationsFromDateByClinicIdAsync(searchId, It.IsAny<DateTime>()))
                .ReturnsAsync(ExaminationsList()
                .Where(x => x.ClinicId == searchId && x.IsPaid == true));
            // Act
            var result = await service.GetExaminationsFromDateByClinicIdAsync(searchId, DateTime.Now);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void CreateExaminationsAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IExaminationRepository>();
            var service = new ExaminationService(mockRepo.Object);
            // Act
            await service.CreateExaminationsAsync(new ExaminationToPost ("procedure x", DateTime.Now, 100, 2, 2));
            // Assert 
            mockRepo.Verify(mr => mr.CreateExaminationAsync(It.IsAny<ExaminationDataLayer>()));
        }
    }
}
