using BusinesLogic.Abstraction.DTO;
using PersonsAPI.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinesLogic.Abstraction.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonToGet>> GetPersonsAsync();
        Task<IEnumerable<PersonToGet>> GetPersonsByNameAsync(string term);
        Task<PersonToGet> GetPersonByIdAsync(int id);
        Task<IEnumerable<PersonToGet>> GetPersonsByNameWithPaginationAsync(string searchTerm, int skip, int take);
        Task<IEnumerable<PersonToGet>> GetPersonsWithPaginationAsync(int skip, int take);
        Task RegisterPersonAsync(PersonToPost newPerson);
        Task EditPersonAsync(PersonToGet newPerson, int id);
        Task DeletePersonByIdAsync(int id);
        Task SetClinicToPersonByIDAsync(ClinicToGet clinic, PersonToGet personToGet);
    }
}
