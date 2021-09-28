using BusinesLogic.Abstraction.Services;
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
    public class LinkPersonToClinicController : ControllerBase
    {
        private readonly IPersonToClinicService _personToClinic;
        private readonly ServiceProperties _settings;

        public LinkPersonToClinicController(
            ILogger<PersonCRUDController> logger,
            IOptions<ServiceProperties> options,
            IPersonToClinicService personToClinic)
        {
            _personToClinic = personToClinic;
            _settings = options.Value;
        }

        /// <summary>
        /// Записать клиента с Id {personId} в клинику с Id {clinicId}
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [HttpPost("person/id/{personId}/to/clinic/id/{clinicId}")]
        public async Task SetClinicToPersonByID([FromRoute] int personId, [FromRoute] int clinicId)
        {
            await _personToClinic.SetClinicToPersonByIDAsync(personId, clinicId);
        }

        /// <summary>
        /// Получить список клиник, в которые записан клиент с Id {personId}
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("clinics/from/person/id/{personId}")]
        public async Task<IEnumerable<ClinicResponse>> GetClinicFromPersonId([FromRoute] int personId)
        {
            var result = await _personToClinic.GetClinicFromPersonIdAsync(personId);
            return result.Select(p => new ClinicResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();
        }

        /// <summary>
        /// Получить список клиентов, которые записаны в клинику с Id {clinicId}
        /// </summary>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [HttpGet("person/from/clinics/id/{clinicId}")]
        public async Task<IEnumerable<PersonResponse>> GetPersonsFromClinicId([FromRoute] int clinicId)
        {
            var result = await _personToClinic.GetPersonsFromClinicIdAsync(clinicId);
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
        /// Получить часть списка клиник, в которые записана персона с Id {personId}, где 'take' содержит количество Clinic на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("clinics/from/person/id/{personId}/{skip}/{take}")]
        public async Task<IEnumerable<ClinicResponse>> GetClinicFromPersonIdWithPagination([FromRoute] int personId, [FromRoute] int skip, [FromRoute] int take)
        {
            var result = await _personToClinic.GetClinicFromPersonIdWithPaginationAsync(personId, skip, take);
            return result.Select(p => new ClinicResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();
        }

        /// <summary>
        /// Получить часть списка Person, которые записаны в клинику с Id {clinicId}, где 'take' содержит количество Person на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="clinicId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("person/from/clinics/id/{clinicId}/{skip}/{take}")]
        public async Task<IEnumerable<PersonResponse>> GetPersonsFromClinicIdWithPagination([FromRoute] int clinicId, [FromRoute] int skip, [FromRoute] int take)
        {
            var result = await _personToClinic.GetPersonsFromClinicIdWithPaginationAsync(clinicId, skip, take);
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
        /// Удаление записи Person в Clinic по их Id
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        [HttpDelete("delete/link/person/{personId}/clinic/{clinicId}")]
        public async Task DeleteLinkPersonToClinic([FromRoute] int personId, [FromRoute] int clinicId)
        {
            await _personToClinic.DeleteLinkPersonToClinicAsync(personId, clinicId);
        }
    }
}
