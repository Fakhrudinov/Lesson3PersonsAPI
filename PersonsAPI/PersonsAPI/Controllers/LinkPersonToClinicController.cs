using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;
using BusinesLogic.Abstraction.Services;
using BusinesLogic.ValidateControllerData;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonsAPI.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PersonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class LinkPersonToClinicController : ControllerBase
    {
        private readonly IPersonToClinicService _personToClinic;
        private readonly IClinicService _clinicService;
        private readonly IPersonService _personService;
        private readonly ServiceProperties _settings;

        public LinkPersonToClinicController(
            ILogger<PersonCRUDController> logger,
            IOptions<ServiceProperties> options,
            IPersonToClinicService personToClinic,
            IClinicService clinicService,
            IPersonService personService)
        {
            _personToClinic = personToClinic;
            _clinicService = clinicService;
            _personService = personService;
            _settings = options.Value;
        }

        /// <summary>
        /// Записать клиента с Id {personId} в клинику с Id {clinicId}
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [HttpPost("person/id/{personId}/to/clinic/id/{clinicId}")]
        public async Task<IActionResult> SetClinicToPersonByID([FromRoute] int personId, [FromRoute] int clinicId)
        {
            var response = new ValidationResponseModel();

            // простые проверки Id
            JoinPCIDValidationService validatorId = new JoinPCIDValidationService();
            IdIdRequest checkId = new IdIdRequest();
            checkId.ClinicId = clinicId;
            checkId.PersonId = personId;

            var validationIdResult = validatorId.Validate(checkId);
            if (!validationIdResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationIdResult, response);

                return BadRequest(response);
            }

            // проверим id Clinic на наличие в БД
            ClinicByIdValidationAsyncService validatorClinicAsync = new ClinicByIdValidationAsyncService(_clinicService);
            ClinicToGet checkIdClinicexist = new ClinicToGet();
            checkIdClinicexist.Id = clinicId;
            var validationClinicResultAsync = await validatorClinicAsync.ValidateAsync(checkIdClinicexist);

            if (!validationClinicResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationClinicResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("J-100.8 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            // проверим id Person на наличие в БД
            PersonByIdValidationAsyncService validatorAsync = new PersonByIdValidationAsyncService(_personService);
            PersonToGet checkIdexist = new PersonToGet();
            checkIdexist.Id = personId;
            var validationResultAsync = await validatorAsync.ValidateAsync(checkIdexist);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("J-100.9 Person с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            await _personToClinic.SetClinicToPersonByIDAsync(personId, clinicId);
            return Ok();            
        }

        /// <summary>
        /// Получить список клиник, в которые записан клиент с Id {personId}
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("clinics/from/person/id/{personId}")]
        public async Task<IActionResult> GetClinicFromPersonId([FromRoute] int personId)
        {
            var response = new ValidationResponseModel();

            // простые проверки Id
            JoinIDValidationService validatorId = new JoinIDValidationService();

            PersonToGet checkId = new PersonToGet();
            checkId.Id = personId;
            var validationIdResult = validatorId.Validate(checkId);

            if (!validationIdResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationIdResult, response);

                return BadRequest(response);
            }

            // проверим id Person на наличие в БД
            PersonByIdValidationAsyncService validatorAsync = new PersonByIdValidationAsyncService(_personService);
            var validationResultAsync = await validatorAsync.ValidateAsync(checkId);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("J-100.10 Person с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            var result = await _personToClinic.GetClinicFromPersonIdAsync(personId);
            result.Select(p => new ClinicResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();

            return Ok(result);
        }

        /// <summary>
        /// Получить список клиентов, которые записаны в клинику с Id {clinicId}
        /// </summary>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [HttpGet("person/from/clinics/id/{clinicId}")]
        public async Task<IActionResult> GetPersonsFromClinicId([FromRoute] int clinicId)
        {
            var response = new ValidationResponseModel();

            // простые проверки Id
            ClinicIDValidationService validatorId = new ClinicIDValidationService();

            ClinicToGet checkIdexist = new ClinicToGet();
            checkIdexist.Id = clinicId;
            var validationIdResult = validatorId.Validate(checkIdexist);

            if (!validationIdResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationIdResult, response);

                return BadRequest(response);
            }

            // проверим id на наличие в БД
            // https://docs.fluentvalidation.net/en/latest/async.html
            ClinicByIdValidationAsyncService validatorAsync = new ClinicByIdValidationAsyncService(_clinicService);

            var validationResultAsync = await validatorAsync.ValidateAsync(checkIdexist);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("J-100.11 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            var result = await _personToClinic.GetPersonsFromClinicIdAsync(clinicId);
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

        /// <summary>
        /// Получить часть списка клиник, в которые записана персона с Id {personId}, где 'take' содержит количество Clinic на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("clinics/from/person/id/{personId}/{skip}/{take}")]
        public async Task<IActionResult> GetClinicFromPersonIdWithPagination([FromRoute] int personId, [FromRoute] int skip, [FromRoute] int take)
        {
            var response = new ValidationResponseModel();

            IdWithPaginationRequest newSearch = new IdWithPaginationRequest();
            newSearch.Id = personId;
            newSearch.Skip = skip;
            newSearch.Take = take;

            // простые проверки
            JoinWithPaginationValidationService validator = new JoinWithPaginationValidationService();
            var validationResult = validator.Validate(newSearch);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            // проверим id Person на наличие в БД
            PersonByIdValidationAsyncService validatorAsync = new PersonByIdValidationAsyncService(_personService);
            PersonToGet checkId = new PersonToGet();
            checkId.Id = personId;
            var validationResultAsync = await validatorAsync.ValidateAsync(checkId);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("J-100.12 Person с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            var result = await _personToClinic.GetClinicFromPersonIdWithPaginationAsync(personId, skip, take);
            result.Select(p => new ClinicResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();

            return Ok(result);
        }

        /// <summary>
        /// Получить часть списка Person, которые записаны в клинику с Id {clinicId}, где 'take' содержит количество Person на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("person/from/clinics/id/{clinicId}/{skip}/{take}")]
        public async Task<IActionResult> GetPersonsFromClinicIdWithPagination([FromRoute] int clinicId, [FromRoute] int skip, [FromRoute] int take)
        {
            var response = new ValidationResponseModel();

            IdWithPaginationRequest newSearch = new IdWithPaginationRequest();
            newSearch.Id = clinicId;
            newSearch.Skip = skip;
            newSearch.Take = take;

            // простые проверки
            JoinWithPaginationValidationService validator = new JoinWithPaginationValidationService();
            var validationResult = validator.Validate(newSearch);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            // проверим id на наличие в БД
            // https://docs.fluentvalidation.net/en/latest/async.html
            ClinicByIdValidationAsyncService validatorAsync = new ClinicByIdValidationAsyncService(_clinicService);
            ClinicToGet checkId = new ClinicToGet();
            checkId.Id = clinicId;
            var validationResultAsync = await validatorAsync.ValidateAsync(checkId);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("J-100.13 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            var result = await _personToClinic.GetPersonsFromClinicIdWithPaginationAsync(clinicId, skip, take);
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

        /// <summary>
        /// Удаление записи Person в Clinic по их Id
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [HttpDelete("delete/link/person/{personId}/clinic/{clinicId}")]
        public async Task<IActionResult> DeleteLinkPersonToClinic([FromRoute] int personId, [FromRoute] int clinicId)
        {
            var response = new ValidationResponseModel();

            // простые проверки Id
            JoinPCIDValidationService validatorId = new JoinPCIDValidationService();
            IdIdRequest checkId = new IdIdRequest();
            checkId.ClinicId = clinicId;
            checkId.PersonId = personId;

            var validationIdResult = validatorId.Validate(checkId);
            if (!validationIdResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationIdResult, response);

                return BadRequest(response);
            }

            // проверим id Clinic на наличие в БД
            ClinicByIdValidationAsyncService validatorClinicAsync = new ClinicByIdValidationAsyncService(_clinicService);
            ClinicToGet checkIdClinicexist = new ClinicToGet();
            checkIdClinicexist.Id = clinicId;
            var validationClinicResultAsync = await validatorClinicAsync.ValidateAsync(checkIdClinicexist);

            if (!validationClinicResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationClinicResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("J-100.14 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            // проверим id Person на наличие в БД
            PersonByIdValidationAsyncService validatorAsync = new PersonByIdValidationAsyncService(_personService);
            PersonToGet checkIdexist = new PersonToGet();
            checkIdexist.Id = personId;
            var validationResultAsync = await validatorAsync.ValidateAsync(checkIdexist);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("J-100.15 Person с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            await _personToClinic.DeleteLinkPersonToClinicAsync(personId, clinicId);
            return Ok();
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
