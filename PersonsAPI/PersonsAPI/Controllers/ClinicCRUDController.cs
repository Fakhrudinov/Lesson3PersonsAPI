using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;
using BusinesLogic.Abstraction.Services;
using BusinesLogic.ValidateControllerData;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonsAPI.Requests;
using PersonsAPI.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsAPI.Controllers
{
    [Route("clinics")]
    [ApiController]
    [Authorize]

    public class ClinicCRUDController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        private readonly ServiceProperties _settings;

        public ClinicCRUDController(
            IOptions<ServiceProperties> options,
            IClinicService clinicService)
        {
            _clinicService = clinicService;
            _settings = options.Value;
        }
        /// <summary>
        /// Получить полный список клиник
        /// </summary>
        /// <returns></returns>
        [HttpGet("allClinic")]
        public async Task<IEnumerable<ClinicResponse>> GetPersons()
        {
            var result = await _clinicService.GetClinicsAsync();

            return result.Select(p => new ClinicResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();
        }

        /// <summary>
        /// Получить одну клинику по её Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClinicById(int id)
        {
            var response = new ValidationResponseModel();

            ClinicToGet newClinic = new ClinicToGet();
            newClinic.Id = id;

            // простые проверки
            ClinicIDValidationService validator = new ClinicIDValidationService();
            var validationResult = validator.Validate(newClinic);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            else
            {
                var clinicToGet = await _clinicService.GetClinicByIdAsync(id);
                if (clinicToGet.Id == 0)
                {
                    response.IsValid = false;
                    response.ValidationMessages.Add("C-100.18 Clinic с таким Id не существует.");

                    return UnprocessableEntity(response);
                }

                ClinicResponse findedClinic = new ClinicResponse();

                findedClinic.Id = clinicToGet.Id;
                findedClinic.Name = clinicToGet.Name;
                findedClinic.Adress = clinicToGet.Adress;

                return Ok(findedClinic);
            }
        }

        /// <summary>
        /// Получить список клиник содержащих в названии 'searchTerm'. Возвращает список
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("search/")]
        public async Task<IActionResult> GetClinicsByName([FromQuery] string searchTerm)
        {
            var response = new ValidationResponseModel();

            SearchWithPaginationRequest newSearch = new SearchWithPaginationRequest();
            newSearch.SearchTerm = searchTerm;

            // простые проверки
            ClinicSearchValidationService validator = new ClinicSearchValidationService();
            var validationResult = validator.Validate(newSearch);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            else
            {
                var result = await _clinicService.GetClinicsByNameAsync(searchTerm);

                result.Select(p => new ClinicResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Adress = p.Adress,
                }).ToArray();

                return Ok(result);
            }
        }

        /// <summary>
        /// Получить часть списка клиник, содержащих в названии 'searchTerm', где 'take' содержит количество Clinic на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}/{skip}/{take}")]
        public async Task<IActionResult> GetClinicsByNameWithPagination([FromRoute] string searchTerm, [FromRoute] int skip, [FromRoute] int take)
        {
            var response = new ValidationResponseModel();

            SearchWithPaginationRequest newSearch = new SearchWithPaginationRequest();
            newSearch.SearchTerm = searchTerm;
            newSearch.Skip = skip;
            newSearch.Take = take;

            // простые проверки
            ClinicSearchWithPaginationValidationService validator = new ClinicSearchWithPaginationValidationService();
            var validationResult = validator.Validate(newSearch);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            else
            {
                var result = await _clinicService.GetClinicsByNameWithPaginationAsync(searchTerm, skip, take);
                result.Select(p => new ClinicResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Adress = p.Adress,
                }).ToArray();

                return Ok(result);
            }
        }

        /// <summary>
        /// Получить часть списка клиник, где 'take' содержит количество Clinic на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("{skip}/{take}")]
        public async Task<IActionResult> GetClinicsWithPagination([FromRoute] int skip, [FromRoute] int take)
        {
            var response = new ValidationResponseModel();

            SearchWithPaginationRequest newSearch = new SearchWithPaginationRequest();
            newSearch.Skip = skip;
            newSearch.Take = take;

            // простые проверки
            ClinicSearchWithPaginationValidationService validator = new ClinicSearchWithPaginationValidationService();
            var validationResult = validator.Validate(newSearch);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            else
            {
                var result = await _clinicService.GetClinicsWithPaginationAsync(skip, take);
                result.Select(p => new ClinicResponse()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Adress = p.Adress,
                }).ToArray();

                return Ok(result);
            }
        }

        /// <summary>
        /// Добавить новую клинику
        /// </summary>
        /// <param name="clinic"></param>
        /// <returns></returns>
        [HttpPost("post")]
        public async Task<IActionResult> RegisterClinic([FromBody] Clinic clinic)
        {
            var response = new ValidationResponseModel();

            ClinicToPost newClinic = new ClinicToPost();

            newClinic.Name = clinic.Name;
            newClinic.Adress = clinic.Adress;

            // простые проверки
            ClinicValidationService validator = new ClinicValidationService();
            var validationResult = validator.Validate(newClinic);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            // проверим имя на наличие в БД
            // https://docs.fluentvalidation.net/en/latest/async.html
            ClinicValidationAsyncService validatorAsync = new ClinicValidationAsyncService(_clinicService);
            var validationResultAsync = await validatorAsync.ValidateAsync(newClinic);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                return UnprocessableEntity(response);
            }
            else
            {
                await _clinicService.RegisterClinicAsync(newClinic);
                return Ok(newClinic);
            }
        }

        /// <summary>
        /// Редактирование данных существующей клиники
        /// </summary>
        /// <param name="clinic"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> EditClinic([FromBody] ClinicResponse clinic)
        {
            var response = new ValidationResponseModel();

            ClinicToPost newClinic = new ClinicToPost();

            newClinic.Name = clinic.Name;
            newClinic.Adress = clinic.Adress;

            // простые проверки
            ClinicValidationService validator = new ClinicValidationService();
            var validationResult = validator.Validate(newClinic);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            ClinicToGet newClinicId = new ClinicToGet();
            newClinicId.Id = clinic.Id;

            // простые проверки Id
            ClinicIDValidationService validatorId = new ClinicIDValidationService();

            ClinicToGet checkIdexist = new ClinicToGet();
            checkIdexist.Id = clinic.Id;
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
                response.ValidationMessages.Add("C-100.13 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }
            else
            {
                ClinicToGet editClinic = new ClinicToGet();

                editClinic.Name = clinic.Name;
                editClinic.Adress = clinic.Adress;

                await _clinicService.EditClinicAsync(editClinic, clinic.Id);

                return Ok(editClinic);
            }
        }

        /// <summary>
        /// Удаление существующей клиники
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinicById([FromRoute] int id)
        {
            var response = new ValidationResponseModel();

            // простые проверки Id
            ClinicIDValidationService validatorId = new ClinicIDValidationService();

            ClinicToGet checkIdexist = new ClinicToGet();
            checkIdexist.Id = id;
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
                response.ValidationMessages.Add("C-100.14 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }
            else
            {
                await _clinicService.DeleteClinicByIdAsync(id);

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
