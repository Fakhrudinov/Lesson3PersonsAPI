using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;
using BusinesLogic.Abstraction.Services;
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
        public async Task<ClinicResponse> GetClinicById(int id)
        {
            var clinicToGet = await _clinicService.GetClinicByIdAsync(id);

            ClinicResponse findedClinic = new ClinicResponse();

            findedClinic.Id = clinicToGet.Id;
            findedClinic.Name = clinicToGet.Name;
            findedClinic.Adress = clinicToGet.Adress;

            return findedClinic;
        }

        /// <summary>
        /// Получить список клиник содержащих в названии 'searchTerm'. Возвращает список
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("search/")]
        public async Task<IEnumerable<ClinicResponse>> GetClinicsByName([FromQuery] string searchTerm)
        {
            var result = await _clinicService.GetClinicsByNameAsync(searchTerm);
            return result.Select(p => new ClinicResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();
        }

        /// <summary>
        /// Получить часть списка клиник, содержащих в названии 'searchTerm', где 'take' содержит количество Clinic на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}/{skip}/{take}")]
        public async Task<IEnumerable<ClinicResponse>> GetClinicsByNameWithPagination([FromRoute] string searchTerm, [FromRoute] int skip, [FromRoute] int take)
        {
            var result = await _clinicService.GetClinicsByNameWithPaginationAsync(searchTerm, skip, take);
            return result.Select(p => new ClinicResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();
        }

        /// <summary>
        /// Получить часть списка клиник, где 'take' содержит количество Clinic на странице и 'skip' количество пропущеных страниц. 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet("{skip}/{take}")]
        public async Task<IEnumerable<ClinicResponse>> GetClinicsWithPagination([FromRoute] int skip, [FromRoute] int take)
        {
            var result = await _clinicService.GetClinicsWithPaginationAsync(skip, take);
            return result.Select(p => new ClinicResponse()
            {
                Id = p.Id,
                Name = p.Name,
                Adress = p.Adress,
            }).ToArray();
        }

        /// <summary>
        /// Добавить новую клинику
        /// </summary>
        /// <param name="clinic"></param>
        /// <returns></returns>
        [HttpPost("post")]
        public async Task RegisterClinic([FromBody] Clinic clinic)
        {
            ClinicToPost newClinic = new ClinicToPost();

            newClinic.Name = clinic.Name;
            newClinic.Adress = clinic.Adress;

            await _clinicService.RegisterClinicAsync(newClinic);
        }

        /// <summary>
        /// Редактирование данных существующей клиники
        /// </summary>
        /// <param name="clinic"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task EditClinic([FromBody] ClinicResponse clinic)
        {
            ClinicToGet editClinic = new ClinicToGet();

            editClinic.Name = clinic.Name;
            editClinic.Adress = clinic.Adress;

            await _clinicService.EditClinicAsync(editClinic, clinic.Id);
        }

        /// <summary>
        /// Удаление существующей клиники
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteClinicById([FromRoute] int id)
        {
            await _clinicService.DeleteClinicByIdAsync(id);
        }
    }
}
