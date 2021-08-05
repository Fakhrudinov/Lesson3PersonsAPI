using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Services;
using DataLayer.Abstraction.Entityes;
using DataLayer.Abstraction.Repositories;
using PersonsAPI.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<PersonToGet>> GetPersonsAsync()
        {
            var result = await _repository.GetPersonsAsync();
            return result.Select(p => new PersonToGet()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        public async Task<PersonToGet> GetPersonByIdAsync(int id)
        {
            var result = await _repository.GetPersonByIdAsync(id);

            PersonToGet findedPerson = new PersonToGet();

            if (result != null)
            {
                findedPerson.Id = result.Id;
                findedPerson.FirstName = result.FirstName;
                findedPerson.LastName = result.LastName;
                findedPerson.Age = result.Age;
                findedPerson.Email = result.Email;
                findedPerson.Company = result.Company;
            }
            return findedPerson;
        }

        public async Task<IEnumerable<PersonToGet>> GetPersonsByNameWithPaginationAsync(string searchTerm, int skip, int take)
        {
            var result = await _repository.GetPersonsByNameWithPaginationAsync(searchTerm, skip, take);
            return result.Select(p => new PersonToGet()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        public async Task<IEnumerable<PersonToGet>> GetPersonsByNameAsync(string term)
        {
            var result = await _repository.GetPersonsByNameAsync(term);
            return result.Select(p => new PersonToGet()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        public async Task<IEnumerable<PersonToGet>> GetPersonsWithPaginationAsync(int skip, int take)
        {
            var result = await _repository.GetPersonsWithPaginationAsync(skip, take);
            return result.Select(p => new PersonToGet()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        public async Task RegisterPersonAsync(PersonToPost person)
        {
            PersonDataLayer newPerson = new PersonDataLayer();

            newPerson.FirstName = person.FirstName;
            newPerson.LastName = person.LastName;
            newPerson.Age = person.Age;
            newPerson.Email = person.Email;
            newPerson.Company = person.Company;

            await _repository.RegisterPersonAsync(newPerson);
        }

        public async Task EditPersonAsync(PersonToGet person, int id)
        {
            PersonDataLayer newPerson = new PersonDataLayer();

            newPerson.FirstName = person.FirstName;
            newPerson.LastName = person.LastName;
            newPerson.Age = person.Age;
            newPerson.Email = person.Email;
            newPerson.Company = person.Company;

            await _repository.EditPersonAsync(newPerson, id);
        }

        public async Task DeletePersonByIdAsync(int id)
        {
            await _repository.DeletePersonByIdAsync(id);
        }
    }   
}
