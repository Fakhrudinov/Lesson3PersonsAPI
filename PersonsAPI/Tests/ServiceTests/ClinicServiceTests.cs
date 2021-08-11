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

namespace Tests
{
    public class ClinicServiceTests
    {
        private IEnumerable<ClinicDataLayer> ClinicList()
        {
            IList<ClinicDataLayer> clinics = new List<ClinicDataLayer>
                {
                new ClinicDataLayer { Id = 1, Name = "Eye Clinic1", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 2, Name = "Eye Clinic2", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 3, Name = "Eye Clinic3", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 4, Name = "Eye Clinic4", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 5, Name = "Tooth Clinic1", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 6, Name = "Tooth Clinic2", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 7, Name = "Tooth Clinic3", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 8, Name = "Tooth Clinic4", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 9, Name = "ass Clinic1 ", Adress = "132123 444ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 10, Name = "ass Clinic2", Adress = "132123 333ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 11, Name = "ass Clinic3", Adress = "132123 444ывадтфывафыва 1" },
                new ClinicDataLayer { Id = 12, Name = "ass Clinic4", Adress = "132123 555ывадтфывафыва 1" }
                };
            return clinics;
        }

        [Fact]
        public async Task GetClinicsAsync_Return_NotEmpty()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            mockRepo
                .Setup(repo => repo.GetClinicsAsync())
                .ReturnsAsync(ClinicList());
            var service = new ClinicService(mockRepo.Object);
            // Act
            var result = await service.GetClinicsAsync();
            // Assert 
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetClinicsAsync_Return12Clinics_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            mockRepo
                .Setup(repo => repo.GetClinicsAsync())
                .ReturnsAsync(ClinicList());
            var service = new ClinicService(mockRepo.Object);
            // Act
            var result = await service.GetClinicsAsync();
            // Assert 
            Assert.Equal(12, result.Count());
        }

        [Fact]
        public async void GetClinicByIdAsync_Return1Clinic_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            int searchId = 2;
            //mockRepo
            //    .Setup(mr => mr.GetClinicByIdAsync(It.IsAny<int>()))
            //    .ReturnsAsync(new ClinicDataLayer { Id = 2, Name = "Eye Clinic2", Adress = "132123 333ывадтфывафыва 1" });
            mockRepo
                .Setup(mr => mr.GetClinicByIdAsync(searchId))
                .ReturnsAsync(ClinicList()
                .Where(x => x.Id == searchId)
                .FirstOrDefault);
            var service = new ClinicService(mockRepo.Object);
            // Act
            var result = await service.GetClinicByIdAsync(searchId);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal("Eye Clinic2", result.Name);
        }

        [Fact]
        public async void GetClinicByIdAsync_ReturnDefaultClinic_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            mockRepo
                .Setup(mr => mr.GetClinicByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ClinicDataLayer { Id = 0, Name = null, Adress = null });
            var service = new ClinicService(mockRepo.Object);
            // Act
            var result = await service.GetClinicByIdAsync(1231231231);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
        }

        [Fact]
        public async void GetClinicsByNameWithPaginationAsync_Return2Clinics_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            mockRepo
                .Setup(mr => mr.GetClinicsByNameWithPaginationAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(ClinicList()
                    .Where(x => x.Name.Contains("Tooth")
                ).Skip(2).Take(2)); 
            var service = new ClinicService(mockRepo.Object);
            // Act
            var result = await service.GetClinicsByNameWithPaginationAsync("Tooth", 2, 2);
            // Assert 
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetClinicsWithPaginationAsync_Return3Clinics_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            mockRepo
                .Setup(mr => mr.GetClinicsWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(ClinicList().Skip(3).Take(3));
            var service = new ClinicService(mockRepo.Object);
            // Act
            var result = await service.GetClinicsWithPaginationAsync(3, 3);
            // Assert 
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void GetClinicsByNameAsync_Return4Clinic_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            mockRepo
                .Setup(mr => mr.GetClinicsByNameAsync(It.IsAny<string>()))
                    .ReturnsAsync(ClinicList()
                    .Where(x => x.Name.Contains("Tooth")));
            var service = new ClinicService(mockRepo.Object);
            // Act
            var result = await service.GetClinicsByNameAsync("Tooth");
            // Assert 
            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async void DeleteClinicByIdAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            var service = new ClinicService(mockRepo.Object);
            // Act
            await service.DeleteClinicByIdAsync(1);
            // Assert 
            mockRepo.Verify(mr => mr.DeleteClinicByIdAsync(It.IsAny<int>()));
        }

        [Fact]
        public async void RegisterClinicAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            var service = new ClinicService(mockRepo.Object);
            // Act
            await service.RegisterClinicAsync(new ClinicToPost { Name = null, Adress = null });
            // Assert 
            mockRepo.Verify(mr => mr.RegisterClinicAsync(It.IsAny<ClinicDataLayer>()));
        }

        [Fact]
        public async void EditClinicAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IClinicRepository>();
            var service = new ClinicService(mockRepo.Object);
            // Act
            await service.EditClinicAsync(new ClinicToGet { Name = null, Adress = null }, 1);
            // Assert 
            mockRepo.Verify(mr => mr.EditClinicAsync(It.IsAny<ClinicDataLayer>(), It.IsAny<int>()));
        }
    }  
}
