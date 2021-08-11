using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Abstraction.Repositories;
using DataLayer.Abstraction.Entityes;
using BusinesLogic.Services;

namespace Tests
{
    public class PersonToClinicServiceTests
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

        private IEnumerable<PersonDataLayer> PersonsList()
        {
            IList<PersonDataLayer> persons = new List<PersonDataLayer>
                {
                    new PersonDataLayer { Id = 1, FirstName = "Veda", LastName = "Richmond", Email = "ligula@necluctus.edu", Company = "Quisque Ac Libero LLP", Age = 42 },
                    new PersonDataLayer { Id = 2, FirstName = "Demetria", LastName = "Andrews", Email = "feugiat.metus@penatibuset.org", Company = "Nulla Facilisi Foundation", Age = 31 },
                    new PersonDataLayer { Id = 3, FirstName = "Byron", LastName = "Holmes", Email = "neque.Sed.eget@non.co.uk", Company = "Et Associates", Age = 63 },
                    new PersonDataLayer { Id = 4, FirstName = "Alexander", LastName = "Cummingso", Email = "egestas.ligula@ultricesDuisvolutpat.ca", Company = "Vel Institute", Age = 23 },
                    new PersonDataLayer { Id = 5, FirstName = "Melinda", LastName = "Mileso", Email = "justo.nec.ante@nonummyFusce.ca", Company = "Eu Nibh Vulputate Company", Age = 64 },
                    new PersonDataLayer { Id = 6, FirstName = "Dustin", LastName = "Beck", Email = "iaculis@afeugiat.edu", Company = "Nec Diam Incorporated", Age = 35 },
                    new PersonDataLayer { Id = 7, FirstName = "Ralph", LastName = "Maddox", Email = "ipsum@vulputatelacus.co.uk", Company = "Enim Corp.", Age = 22 },
                    new PersonDataLayer { Id = 8, FirstName = "Levi", LastName = "Zamora", Email = "lectus.a.sollicitudin@nuncQuisque.com", Company = "Sodales At Velit Corp.", Age = 57 },
                    new PersonDataLayer { Id = 9, FirstName = "Drisocoll", LastName = "Esotrada", Email = "Phasellus@Craspellentesque.org", Company = "Id Mollis Nec LLC", Age = 37 },
                    new PersonDataLayer { Id = 10, FirstName = "Hiram", LastName = "Mejia", Email = "lacus.Mauris@semper.ca", Company = "Donec Tincidunt Donec Industries", Age = 59 },
                    new PersonDataLayer { Id = 11, FirstName = "Mason", LastName = "Jefferson", Email = "Integer.vitae.nibh@nibh.co.uk", Company = "Lectus Justo Ltd", Age = 65 },
                    new PersonDataLayer { Id = 12, FirstName = "Nigel", LastName = "Rich", Email = "id@faucibusleoin.net", Company = "Tristique Ac Ltd", Age = 52 },
                };
            return persons;
        }

        [Fact]
        public async void GetClinicFromPersonIdAsync_Return2Clinic_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonToClinicRepository>();
            var service = new PersonToClinicService(mockRepo.Object);
            mockRepo
                .Setup(mr => mr.GetClinicFromPersonIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(ClinicList().Take(2));
            // Act
            var result = await service.GetClinicFromPersonIdAsync(1);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetPersonsFromClinicIdAsync_Return2Persons_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonToClinicRepository>();
            var service = new PersonToClinicService(mockRepo.Object);
            mockRepo
                .Setup(mr => mr.GetPersonsFromClinicIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(PersonsList().Take(2));
            // Act
            var result = await service.GetPersonsFromClinicIdAsync(1);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetClinicFromPersonIdWithPaginationAsync_Return3Clinics_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonToClinicRepository>();
            mockRepo
                .Setup(mr => mr.GetClinicFromPersonIdWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(ClinicList().Skip(3).Take(3));
            var service = new PersonToClinicService(mockRepo.Object);
            // Act
            var result = await service.GetClinicFromPersonIdWithPaginationAsync(2, 3, 3);
            // Assert 
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void GetPersonsFromClinicIdWithPaginationAsync_Return3Persons_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonToClinicRepository>();
            var service = new PersonToClinicService(mockRepo.Object);
            mockRepo
                .Setup(mr => mr.GetPersonsFromClinicIdWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(PersonsList().Skip(3).Take(3));
            // Act
            var result = await service.GetPersonsFromClinicIdWithPaginationAsync(2, 3, 3);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void DeleteClinicByIdAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonToClinicRepository>();
            var service = new PersonToClinicService(mockRepo.Object);
            // Act
            await service.DeleteLinkPersonToClinicAsync(1,1);
            // Assert 
            mockRepo.Verify(mr => mr.DeleteLinkPersonToClinicAsync(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Fact]
        public async void SetClinicToPersonByIDAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonToClinicRepository>();
            var service = new PersonToClinicService(mockRepo.Object);
            // Act
            await service.SetClinicToPersonByIDAsync(1, 1);
            // Assert 
            mockRepo.Verify(mr => mr.SetClinicToPersonByIDAsync(It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}
