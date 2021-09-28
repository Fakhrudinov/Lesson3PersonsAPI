using BusinesLogic.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonsAPI.Requests;
using PersonsAPI.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinesLogic.Abstraction.DTO;

namespace PersonsAPI.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonCRUDController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ServiceProperties _settings;

        public PersonCRUDController(
            IOptions<ServiceProperties> options, 
            IPersonService personService)
        {
            _personService = personService;
            _settings = options.Value;
        }

        /// <summary>
        /// Получить полный список Person. Возвращает список
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IEnumerable<PersonResponse>>  GetPersons()
        {
            var result = await _personService.GetPersonsAsync();
            return result.Select(p => new PersonResponse()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        /// <summary>
        /// Запрос поиска Person по Id. Возвращает один Person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<PersonResponse> GetPersonById(int id)
        {
            var personToGet = await _personService.GetPersonByIdAsync(id);

            PersonResponse findedPerson = new PersonResponse();

            findedPerson.Id = personToGet.Id;
            findedPerson.FirstName = personToGet.FirstName;
            findedPerson.LastName = personToGet.LastName;
            findedPerson.Age = personToGet.Age;
            findedPerson.Email = personToGet.Email;
            findedPerson.Company = personToGet.Company;

            return findedPerson;
        }

        /// <summary>
        /// Получить список Person содержащих 'searchTerm'. Возвращает список
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("search/")]
        public async Task<IEnumerable<PersonResponse>> GetPersonByName([FromQuery] string searchTerm)
        {
            var result = await _personService.GetPersonsByNameAsync(searchTerm);
            return result.Select(p => new PersonResponse()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        /// <summary>
        /// Получить часть списка имен, содержащих 'searchTerm', где 'take' содержит количество Person на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}/{skip}/{take}")]
        public async Task<IEnumerable<PersonResponse>> GetPersonByNameWithPagination([FromRoute] string searchTerm, [FromRoute] int skip, [FromRoute] int take)
        {
            var result = await _personService.GetPersonsByNameWithPaginationAsync(searchTerm, skip, take);
            return result.Select(p => new PersonResponse()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }


        /// <summary>
        /// Получить часть списка Person, где 'take' содержит количество Person на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("{skip}/{take}")]
        public async Task<IEnumerable<PersonResponse>> GetPersonsWithPagination([FromRoute] int skip, [FromRoute] int take)
        {
            var result = await _personService.GetPersonsWithPaginationAsync(skip, take);
            return result.Select(p => new PersonResponse()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Company = p.Company,
                Age = p.Age
            }).ToArray();
        }

        /// <summary>
        /// Добавить новый объект Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task RegisterPerson([FromBody] Person person)
        {
            PersonToPost newPerson = new PersonToPost();

            newPerson.FirstName = person.FirstName;
            newPerson.LastName = person.LastName;
            newPerson.Age = person.Age;
            newPerson.Email = person.Email;
            newPerson.Company = person.Company;

            await _personService.RegisterPersonAsync(newPerson);
        }

        /// <summary>
        /// Редактирование данных существующего Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task EditPerson([FromBody] PersonResponse person)
        {
            PersonToGet editPerson = new PersonToGet();

            editPerson.FirstName = person.FirstName;
            editPerson.LastName = person.LastName;
            editPerson.Age = person.Age;
            editPerson.Email = person.Email;
            editPerson.Company = person.Company;

            await _personService.EditPersonAsync(editPerson, person.Id);
        }


        /// <summary>
        /// Удаление существующего Person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeletePersonById([FromRoute] int id)
        {
            await _personService.DeletePersonByIdAsync(id);
        }
    }
}
