using BusinesLogic.Abstraction.Requests;
using BusinesLogic.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ClinicCRUDController> _logger;
        private readonly IClinicService _clinicService;
        private readonly ServiceProperties _settings;

        public ClinicCRUDController(
            ILogger<ClinicCRUDController> logger,
            IOptions<ServiceProperties> options,
            IClinicService clinicService)
        {
            _logger = logger;
            _clinicService = clinicService;
            _settings = options.Value;
        }

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


        [HttpPost("post")]
        public async Task RegisterClinic([FromBody] Clinic clinic)
        {
            ClinicToPost newClinic = new ClinicToPost();

            newClinic.Name = clinic.Name;
            newClinic.Adress = clinic.Adress;

            await _clinicService.RegisterClinicAsync(newClinic);
        }

    }
}
