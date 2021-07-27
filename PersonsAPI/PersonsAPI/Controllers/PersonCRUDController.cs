using BusinesLogic.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonsAPI.Requests;
using PersonsAPI.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinesLogic.Abstraction.DTO;
using BusinesLogic.ValidateControllerData;
using FluentValidation.Results;
using BusinesLogic.Abstraction.Requests;
using Microsoft.AspNetCore.Authorization;

namespace PersonsAPI.Controllers
{
    [ApiController]
    [Route("persons")]
    [Authorize]

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
        public async Task<IActionResult> GetPersonById(int id)
        {
            var response = new ValidationResponseModel();

            PersonToGet newPerson = new PersonToGet();
            newPerson.Id = id;

            // простые проверки
            PersonIDValidationService validator = new PersonIDValidationService();
            var validationResult = validator.Validate(newPerson);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            else
            {
                var personToGet = await _personService.GetPersonByIdAsync(id);
                if(personToGet.Id == 0)
                {
                    response.IsValid = false;
                    response.ValidationMessages.Add("P-100.18 Person с таким Id не существует.");

                    return UnprocessableEntity(response);
                }

                PersonResponse findedPerson = new PersonResponse();
                findedPerson.Id = personToGet.Id;
                findedPerson.FirstName = personToGet.FirstName;
                findedPerson.LastName = personToGet.LastName;
                findedPerson.Age = personToGet.Age;
                findedPerson.Email = personToGet.Email;
                findedPerson.Company = personToGet.Company;

                return Ok(findedPerson);
            }
        }

        /// <summary>
        /// Получить список Person содержащих 'searchTerm'. Возвращает список
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> GetPersonByName([FromRoute] string searchTerm)
        {
            var response = new ValidationResponseModel();

            SearchWithPaginationRequest newSearch = new SearchWithPaginationRequest();
            newSearch.SearchTerm = searchTerm;

            // простые проверки
            PersonSearchValidationService validator = new PersonSearchValidationService();
            var validationResult = validator.Validate(newSearch);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            else
            {
                var result = await _personService.GetPersonsByNameAsync(searchTerm);
                result.Select(p => new PersonResponse()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Company = p.Company,
                    Age = p.Age
                }).ToArray();

                return Ok(result);
            }
        }

        /// <summary>
        /// Получить часть списка имен, содержащих 'searchTerm', где 'take' содержит количество Person на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}/{skip}/{take}")]
        public async Task<IActionResult> GetPersonByNameWithPagination([FromRoute] string searchTerm, [FromRoute] int skip, [FromRoute] int take)
        {
            var response = new ValidationResponseModel();

            SearchWithPaginationRequest newSearch = new SearchWithPaginationRequest();
            newSearch.SearchTerm = searchTerm;
            newSearch.Skip = skip;
            newSearch.Take = take;

            // простые проверки
            PersonSearchWithPaginationValidationService validator = new PersonSearchWithPaginationValidationService();
            var validationResult = validator.Validate(newSearch);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            else
            {
                var result = await _personService.GetPersonsByNameWithPaginationAsync(searchTerm, skip, take);
                result.Select(p => new PersonResponse()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Company = p.Company,
                    Age = p.Age
                }).ToArray();

                return Ok(result);
            }
        }


        /// <summary>
        /// Получить часть списка Person, где 'take' содержит количество Person на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("{skip}/{take}")]
        public async Task<IActionResult> GetPersonsWithPagination([FromRoute] int skip, [FromRoute] int take)
        {
            var response = new ValidationResponseModel();

            SearchWithPaginationRequest newSearch = new SearchWithPaginationRequest();
            newSearch.Skip = skip;
            newSearch.Take = take;

            // простые проверки
            PersonSearchWithPaginationValidationService validator = new PersonSearchWithPaginationValidationService();
            var validationResult = validator.Validate(newSearch);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            else
            {
                var result = await _personService.GetPersonsWithPaginationAsync(skip, take);
                result.Select(p => new PersonResponse()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    Company = p.Company,
                    Age = p.Age
                }).ToArray();

                return Ok(result);
            }
        }

        /// <summary>
        /// Добавить новый объект Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterPerson([FromBody] Person person)
        {
            var response = new ValidationResponseModel();

            PersonToPost newPerson = new PersonToPost();

            newPerson.FirstName = person.FirstName;
            newPerson.LastName = person.LastName;
            newPerson.Age = person.Age;
            newPerson.Email = person.Email;
            newPerson.Company = person.Company;

            // простые проверки
            PersonValidationService validator = new PersonValidationService();
            var validationResult = validator.Validate(newPerson);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            // проверим имя на наличие в БД
            // https://docs.fluentvalidation.net/en/latest/async.html
            PersonValidationAsyncService validatorAsync = new PersonValidationAsyncService(_personService);
            var validationResultAsync = await validatorAsync.ValidateAsync(newPerson);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                return UnprocessableEntity(response);
            }
            else
            {
                await _personService.RegisterPersonAsync(newPerson);
                return Ok(newPerson);
            }
        }

        /// <summary>
        /// Редактирование данных существующего Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> EditPerson([FromBody] PersonResponse person)
        {
            var response = new ValidationResponseModel();

            PersonToPost newPerson = new PersonToPost();
            newPerson.FirstName = person.FirstName;
            newPerson.LastName = person.LastName;
            newPerson.Age = person.Age;
            newPerson.Email = person.Email;
            newPerson.Company = person.Company;

            // простые проверки
            PersonValidationService validator = new PersonValidationService();
            var validationResult = validator.Validate(newPerson);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            PersonToGet newPersonId = new PersonToGet();
            newPersonId.Id = person.Id;

            // простые проверки Id
            PersonIDValidationService validatorId = new PersonIDValidationService();

            PersonToGet checkIdexist = new PersonToGet();
            checkIdexist.Id = person.Id;
            var validationIdResult = validatorId.Validate(checkIdexist);

            if (!validationIdResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationIdResult, response);

                return BadRequest(response);
            }

            // проверим id на наличие в БД
            // https://docs.fluentvalidation.net/en/latest/async.html
            PersonByIdValidationAsyncService validatorAsync = new PersonByIdValidationAsyncService(_personService);

            var validationResultAsync = await validatorAsync.ValidateAsync(checkIdexist);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("P-100.16 Person с таким Id не существует.");

                return UnprocessableEntity(response);
            }
            else
            {
                PersonToGet editPerson = new PersonToGet();                
                editPerson.FirstName = person.FirstName;
                editPerson.LastName = person.LastName;
                editPerson.Age = person.Age;
                editPerson.Email = person.Email;
                editPerson.Company = person.Company;

                await _personService.EditPersonAsync(editPerson, person.Id);

                return Ok(editPerson);
            }
        }


        /// <summary>
        /// Удаление существующего Person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonById([FromRoute] int id)
        {
            var response = new ValidationResponseModel();

            // простые проверки Id
            PersonIDValidationService validatorId = new PersonIDValidationService();

            PersonToGet checkIdexist = new PersonToGet();
            checkIdexist.Id = id;
            var validationIdResult = validatorId.Validate(checkIdexist);

            if (!validationIdResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationIdResult, response);

                return BadRequest(response);
            }

            // проверим id на наличие в БД
            // https://docs.fluentvalidation.net/en/latest/async.html
            PersonByIdValidationAsyncService validatorAsync = new PersonByIdValidationAsyncService(_personService);

            var validationResultAsync = await validatorAsync.ValidateAsync(checkIdexist);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("P-100.17 Person с таким Id не существует.");

                return UnprocessableEntity(response);
            }
            else
            {
                await _personService.DeletePersonByIdAsync(id);

                return Ok();
            }
        }


        private ValidationResponseModel SetResponseFromValidationResult(ValidationResult validationResultAsync, ValidationResponseModel response)
        {
            List<string> ValidationMessages = new List<string>();

            response.IsValid = false;
            foreach (ValidationFailure failure in validationResultAsync.Errors)
            {
                ValidationMessages.Add(failure.ErrorCode + " " + failure.ErrorMessage);
            }
            response.ValidationMessages = ValidationMessages;

            return response;
        }
    }
}
