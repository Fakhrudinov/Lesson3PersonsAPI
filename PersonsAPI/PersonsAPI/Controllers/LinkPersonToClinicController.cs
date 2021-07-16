using BusinesLogic.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using BusinesLogic.Abstraction.DTO;
using PersonsAPI.Responses;

namespace PersonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkPersonToClinicController : ControllerBase
    {
        private readonly ILogger<PersonCRUDController> _logger;
        private readonly IPersonService _personService;
        private readonly IClinicService _clinicService;
        private readonly ServiceProperties _settings;

        public LinkPersonToClinicController(
            ILogger<PersonCRUDController> logger,
            IOptions<ServiceProperties> options,
            IPersonService personService,
            IClinicService clinicService)
        {
            _logger = logger;
            _personService = personService;
            _settings = options.Value;
            _clinicService = clinicService;
        }


        [HttpPost("setClinic2Person")]
        public async Task<IActionResult> SetClinicToPersonByID([FromRoute] int clientId, [FromRoute] int clinicId)
        {
            //var clinicToGet = await _clinicService.GetClinicByIdAsync(clinicId);
            //var personToGet = await _personService.GetPersonByIdAsync(clientId);

            ClinicToGet findedClinic = new ClinicToGet();
            findedClinic.Id = 4;
            findedClinic.Name = "Clinic4";
            findedClinic.Adress = "132123 444ывадтфывафыва 1";

            PersonToGet newPerson = new PersonToGet();
            newPerson.Id = 31;
            newPerson.FirstName = "Ivor";
            newPerson.LastName = "Lara";
            newPerson.Age = 30;
            newPerson.Email = "Proin.ultrices.Duis@lacuspede.com";
            newPerson.Company = "Ante Foundation";


            await _personService.SetClinicToPersonByIDAsync(findedClinic, newPerson);
            return Ok(findedClinic);
        }
    }
}
