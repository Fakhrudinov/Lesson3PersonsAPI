using BusinesLogic.Abstraction.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinesLogic.Abstraction.Services
{
    public interface IPersonToClinicService
    {
        Task SetClinicToPersonByIDAsync(int personId, int clinicId);
        Task<IEnumerable<ClinicToGet>> GetClinicFromPersonIdAsync(int personId);
        Task<IEnumerable<PersonToGet>> GetPersonsFromClinicIdAsync(int clinicId);
        Task<IEnumerable<ClinicToGet>> GetClinicFromPersonIdWithPaginationAsync(int personId, int skip, int take);
        Task<IEnumerable<PersonToGet>> GetPersonsFromClinicIdWithPaginationAsync(int clinicId, int skip, int take);
        Task DeleteLinkPersonToClinicAsync(int personId, int clinicId);
    }
}
