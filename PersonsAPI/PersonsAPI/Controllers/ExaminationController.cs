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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationService _examinationService;
        private readonly IClinicService _clinicService;
        private readonly IPersonService _personService;
        private readonly ServiceProperties _settings;

        public ExaminationController(
            IOptions<ServiceProperties> options,
            IExaminationService examinationService,
            IClinicService clinicService,
            IPersonService personService)
        {
            _examinationService = examinationService;
            _clinicService = clinicService;
            _personService = personService;
            _settings = options.Value;
        }

        /// <summary>
        /// Получить полный список Examination. Возвращает список
        /// </summary>
        [HttpGet("all")]
        public async Task<IEnumerable<ExaminationResponse>> GetExaminations()
        {
            var result = await _examinationService.GetAllExaminationsAsync();
            return result.Select(c => new ExaminationResponse()
            {
                Id = c.Id,
                ProcedureName = c.ProcedureName,
                ProcedureDate = c.ProcedureDate,
                ProcedureCost = c.ProcedureCost,
                IsPaid = c.IsPaid,
                PaidDate = c.PaidDate,
                PersonId = c.PersonId,
                ClinicId = c.ClinicId
            }).ToArray();
        }

        /// <summary>
        /// Создать новую запись о приёме. При успешном создании в ответ вернется id новой записи
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateExamination([FromBody] NewExamination examination)
        {
            var response = new ValidationResponseModel();

            var newExamination = new ExaminationToPost(
                examination.ProcedureName,
                examination.ProcedureDate,
                examination.ProcedureCost,
                examination.PersonId,
                examination.ClinicId);

            // проверим поля нового Examination
            ExaminationValidationService validator = new ExaminationValidationService();
            var validationResult = validator.Validate(newExamination);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            // проверим id на наличие в БД
            //clinic
            ClinicByIdValidationAsyncService validatorCliAsync = new ClinicByIdValidationAsyncService(_clinicService);
            ClinicToGet checkCliIdexist = new ClinicToGet();
            checkCliIdexist.Id = examination.ClinicId;
            var validationCliResultAsync = await validatorCliAsync.ValidateAsync(checkCliIdexist);
            if (!validationCliResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationCliResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("E-100.13 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }
            // проверим id на наличие в БД
            //person
            PersonByIdValidationAsyncService validatorAsync = new PersonByIdValidationAsyncService(_personService);
            PersonToGet checkIdexist = new PersonToGet();
            checkIdexist.Id = examination.PersonId;
            var validationResultAsync = await validatorAsync.ValidateAsync(checkIdexist);
            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("E-100.14 Person с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            // добавим новую запись
            var id = await _examinationService.CreateExaminationsAsync(newExamination);
            return Ok(id);
        }

        /// <summary>
        /// Получить 1 запись о приеме по её Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExaminationById(int id)
        {
            var response = new ValidationResponseModel();

            // простые проверки
            //проверим id
            ExaminationToGet newExamination = new ExaminationToGet();
            newExamination.Id = id;
            ExaminationIDValidationService validator = new ExaminationIDValidationService();
            var validationResult = validator.Validate(newExamination);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            //проверим наличие записи с id в базе
            ExaminationToGet examinationToGet = await _examinationService.GetExaminationByIdAsync(id);
            if (examinationToGet.Id == 0)
            {
                response.IsValid = false;
                response.ValidationMessages.Add("E-100.15 Examination с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            ExaminationResponse findedExamination = new ExaminationResponse();
            findedExamination.Id = examinationToGet.Id;
            findedExamination.ProcedureName = examinationToGet.ProcedureName;
            findedExamination.ProcedureDate = examinationToGet.ProcedureDate;
            findedExamination.ProcedureCost = examinationToGet.ProcedureCost;
            findedExamination.IsPaid = examinationToGet.IsPaid;
            findedExamination.PaidDate = examinationToGet.PaidDate;
            findedExamination.PersonId = examinationToGet.PersonId;
            findedExamination.ClinicId = examinationToGet.ClinicId;

            return Ok(findedExamination);            
        }


        /// <summary>
        /// Для записи о приёме с указанным Id установить метку "оплачено"
        /// </summary>
        [HttpPut("paid/id/{id}")]
        public async Task<IActionResult> SetPaimentToExaminationId(int id)
        {
            var response = new ValidationResponseModel();

            // простые проверки
            //проверим id
            ExaminationToGet newExamination = new ExaminationToGet();
            newExamination.Id = id;
            ExaminationIDValidationService validator = new ExaminationIDValidationService();
            var validationResult = validator.Validate(newExamination);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            //проверим наличие записи с id в базе
            ExaminationToGet examinationToGet = await _examinationService.GetExaminationByIdAsync(id);
            if (examinationToGet.Id == 0)
            {
                response.IsValid = false;
                response.ValidationMessages.Add("E-100.16 Examination с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            await _examinationService.SetPaimentToExaminationIdAsync(id);

            return Ok();            
        }

        /// <summary>
        /// Получить все Examinations для клиента с Id, отбирая в isPaid 'true'=оплаченные или 'false'=неоплаченные. Возвращает список
        /// </summary>
        [HttpGet("client/{id}/paid/{isPaid}")]
        public async Task<IActionResult> GetExaminationsByClientId(int id, bool isPaid)
        {
            var response = new ValidationResponseModel();

            // простые проверки
            //проверим id
            ExaminationToGet newExamination = new ExaminationToGet();
            newExamination.Id = id;
            ExaminationIDValidationService validator = new ExaminationIDValidationService();
            var validationResult = validator.Validate(newExamination);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            //проверим bool
            ExaminationBoolValidationService validatorBool = new ExaminationBoolValidationService();
            validationResult = validatorBool.Validate(isPaid);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            // проверим id на наличие в БД
            //person
            PersonByIdValidationAsyncService validatorAsync = new PersonByIdValidationAsyncService(_personService);
            PersonToGet checkIdexist = new PersonToGet();
            checkIdexist.Id = id;
            var validationResultAsync = await validatorAsync.ValidateAsync(checkIdexist);
            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("E-100.17 Person с таким Id не существует.");

                return UnprocessableEntity(response);
            }

            IEnumerable<ExaminationToGet> result = await _examinationService.GetExaminationsByClientIdAsync(id, isPaid);

            result.Select(c => new ExaminationResponse()
            {
                Id = c.Id,
                ProcedureName = c.ProcedureName,
                ProcedureDate = c.ProcedureDate,
                ProcedureCost = c.ProcedureCost,
                IsPaid = c.IsPaid,
                PaidDate = c.PaidDate,
                PersonId = c.PersonId,
                ClinicId = c.ClinicId
            }).ToArray();

            return Ok(result);
        }

        /// <summary>
        /// Получить все Examinations для Clinic с Id, отбирая в isPaid 'true'=оплаченные или 'false'=неоплаченные. Возвращает список
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isPaid"></param>
        /// <returns></returns>
        [HttpGet("clinic/{id}/paid/{isPaid}")]
        public async Task<IActionResult> GetExaminationsByClinicId(int id, bool isPaid)
        {
            var response = new ValidationResponseModel();

            // простые проверки
            //проверим id
            ExaminationToGet newExamination = new ExaminationToGet();
            newExamination.Id = id;
            ExaminationIDValidationService validator = new ExaminationIDValidationService();
            var validationResult = validator.Validate(newExamination);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            //проверим bool
            ExaminationBoolValidationService validatorBool = new ExaminationBoolValidationService();
            validationResult = validatorBool.Validate(isPaid);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            // проверим id на наличие в БД
            //clinic
            ClinicByIdValidationAsyncService validatorCliAsync = new ClinicByIdValidationAsyncService(_clinicService);
            ClinicToGet checkCliIdexist = new ClinicToGet();
            checkCliIdexist.Id = id;
            var validationCliResultAsync = await validatorCliAsync.ValidateAsync(checkCliIdexist);
            if (!validationCliResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationCliResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("E-100.18 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }


            IEnumerable<ExaminationToGet> result = await _examinationService.GetExaminationsByClinicIdAsync(id, isPaid);

            result.Select(c => new ExaminationResponse()
            {
                Id = c.Id,
                ProcedureName = c.ProcedureName,
                ProcedureDate = c.ProcedureDate,
                ProcedureCost = c.ProcedureCost,
                IsPaid = c.IsPaid,
                PaidDate = c.PaidDate,
                PersonId = c.PersonId,
                ClinicId = c.ClinicId
            }).ToArray();

            return Ok(result);
        }

        /// <summary>
        /// Получить все Examinations для Clinic с Id за указанную в date дату. Возвращает список
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("clinic/{id}/date/{date}")]
        public async Task<IActionResult> GetExaminationsByClinicId(int id, DateTime date)
        {
            var response = new ValidationResponseModel();

            // простые проверки
            //проверим id
            ExaminationToGet newExamination = new ExaminationToGet();
            newExamination.Id = id;
            ExaminationIDValidationService validator = new ExaminationIDValidationService();
            var validationResult = validator.Validate(newExamination);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            //проверка DateTime
            ExaminationDateValidationService validatorDate = new ExaminationDateValidationService();
            validationResult = validatorDate.Validate(date);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            // проверим id на наличие в БД
            //clinic
            ClinicByIdValidationAsyncService validatorCliAsync = new ClinicByIdValidationAsyncService(_clinicService);
            ClinicToGet checkCliIdexist = new ClinicToGet();
            checkCliIdexist.Id = id;
            var validationCliResultAsync = await validatorCliAsync.ValidateAsync(checkCliIdexist);
            if (!validationCliResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationCliResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("E-100.19 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }


            IEnumerable<ExaminationToGet> result = await _examinationService.GetExaminationsFromDateByClinicIdAsync(id, date);

            result.Select(c => new ExaminationResponse()
            {
                Id = c.Id,
                ProcedureName = c.ProcedureName,
                ProcedureDate = c.ProcedureDate,
                ProcedureCost = c.ProcedureCost,
                IsPaid = c.IsPaid,
                PaidDate = c.PaidDate,
                PersonId = c.PersonId,
                ClinicId = c.ClinicId
            }).ToArray();

            return Ok(result);
        }

        // все свободные часы в клинике по id клиники на дату//
        // предположим, что в клинике только 1 доктор и каждое обследование займет 1 час.
        // все назначения начинаются с 00 минут! независимо от того что в записи
        // круглосуточно
        /// <summary>
        /// Получить все свободные часы для Clinic с Id за указанную в date дату. Возвращает список int часов из диапазона 00-->23
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("freehours/fromclinic/{id}/date/{date}")]
        public async Task<IActionResult> GetFreeExaminationsClinicId(int id, DateTime date)
        {
            var response = new ValidationResponseModel();

            // простые проверки
            //проверим id
            ExaminationToGet newExamination = new ExaminationToGet();
            newExamination.Id = id;
            ExaminationIDValidationService validator = new ExaminationIDValidationService();
            var validationResult = validator.Validate(newExamination);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            //проверка DateTime
            ExaminationDateValidationService validatorDate = new ExaminationDateValidationService();
            validationResult = validatorDate.Validate(date);
            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }
            // проверим id на наличие в БД
            //clinic
            ClinicByIdValidationAsyncService validatorCliAsync = new ClinicByIdValidationAsyncService(_clinicService);
            ClinicToGet checkCliIdexist = new ClinicToGet();
            checkCliIdexist.Id = id;
            var validationCliResultAsync = await validatorCliAsync.ValidateAsync(checkCliIdexist);
            if (!validationCliResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationCliResultAsync, response);

                response.IsValid = false;
                response.ValidationMessages.Add("E-100.20 Клиника с таким Id не существует.");

                return UnprocessableEntity(response);
            }


            IEnumerable<int> result = await _examinationService.GetFreeExaminationsByClinicIdAsync(id, date);

            return Ok(result);
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
