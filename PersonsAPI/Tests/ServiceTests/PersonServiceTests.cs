using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Abstraction.Repositories;
using DataLayer.Abstraction.Entityes;
using System.Threading.Tasks;
using BusinesLogic.Services;
using BusinesLogic.Abstraction.DTO;
using PersonsAPI.Requests;

namespace Tests
{
    public class PersonServiceTests
    {
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
        public async Task GetPersonsAsync_Return3Persons_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo
                .Setup(repo => repo.GetPersonsAsync())
                .ReturnsAsync(PersonsList());
            var service = new PersonService(mockRepo.Object);
            // Act
            var result = await service.GetPersonsAsync();
            // Assert 
            Assert.NotEmpty(result);
            Assert.Equal(12, result.Count());
        }

        [Fact]
        public async void GetPersonByIdAsync_Return1Person_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            int searchId = 2;
            mockRepo
                .Setup(mr => mr.GetPersonByIdAsync(searchId))
                .ReturnsAsync(PersonsList()
                .Where(x => x.Id == searchId)
                .FirstOrDefault);
            var service = new PersonService(mockRepo.Object);
            // Act
            var result = await service.GetPersonByIdAsync(searchId);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal("Demetria", result.FirstName);//new PersonDataLayer { Id = 2, FirstName = "Demetria", LastName = "Andrews", Email = "feugiat.metus@penatibuset.org", Company = "Nulla Facilisi Foundation", Age = 31 },
        }

        [Fact]
        public async void GetPersonByIdAsync_ReturnDefaultPerson_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo
                .Setup(mr => mr.GetPersonByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new PersonDataLayer { Id = 0, FirstName = null, LastName = null, Email = null, Company = null, Age = 0 });
            var service = new PersonService(mockRepo.Object);
            // Act
            var result = await service.GetPersonByIdAsync(1231231231);
            // Assert 
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
        }

        [Fact]
        public async void GetPersonsByNameWithPaginationAsync_Return2Persons_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo
                .Setup(mr => mr.GetPersonsByNameWithPaginationAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(PersonsList()
                    .Where(x => x.FirstName.Contains("so") || x.LastName.Contains("so")
                ).Skip(2).Take(2));
            var service = new PersonService(mockRepo.Object);
            // Act
            var result = await service.GetPersonsByNameWithPaginationAsync("so", 2, 2);
            // Assert 
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetPersonsWithPaginationAsync_Return3Persons_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo
                .Setup(mr => mr.GetPersonsWithPaginationAsync(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(PersonsList().Skip(3).Take(3));
            var service = new PersonService(mockRepo.Object);
            // Act
            var result = await service.GetPersonsWithPaginationAsync(3, 3);
            // Assert 
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void GetPersonsByNameAsync_Return4Person_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo
                .Setup(mr => mr.GetPersonsByNameAsync(It.IsAny<string>()))
                    .ReturnsAsync(PersonsList()
                    .Where(x => x.FirstName.Contains("so") || x.LastName.Contains("so")));
            var service = new PersonService(mockRepo.Object);
            // Act
            var result = await service.GetPersonsByNameAsync("so");
            // Assert 
            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async void DeletePersonByIdAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var service = new PersonService(mockRepo.Object);
            // Act
            await service.DeletePersonByIdAsync(1);
            // Assert 
            mockRepo.Verify(mr => mr.DeletePersonByIdAsync(It.IsAny<int>()));
        }

        [Fact]
        public async void RegisterPersonAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var service = new PersonService(mockRepo.Object);
            // Act
            await service.RegisterPersonAsync(new PersonToPost { FirstName = null, LastName = null, Email = null, Company = null, Age = 0 });
            // Assert 
            mockRepo.Verify(mr => mr.RegisterPersonAsync(It.IsAny<PersonDataLayer>()));
        }

        [Fact]
        public async void EditPersonAsync_IsCalled_True()
        {
            // Arrange
            var mockRepo = new Mock<IPersonRepository>();
            var service = new PersonService(mockRepo.Object);
            // Act
            await service.EditPersonAsync(new PersonToGet { FirstName = null, LastName = null, Email = null, Company = null, Age = 0 }, 1);
            // Assert 
            mockRepo.Verify(mr => mr.EditPersonAsync(It.IsAny<PersonDataLayer>(), It.IsAny<int>()));
        }
    }
}
