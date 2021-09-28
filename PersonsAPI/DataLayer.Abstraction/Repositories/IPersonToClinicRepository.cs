using DataLayer.Abstraction.Entityes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Abstraction.Repositories
{
    public interface IPersonToClinicRepository
    {
        Task SetClinicToPersonByIDAsync(int personId, int clinicId);
        Task<IEnumerable<ClinicDataLayer>> GetClinicFromPersonIdAsync(int personId);
        Task<IEnumerable<PersonDataLayer>> GetPersonsFromClinicIdAsync(int clinicId);
        Task<IEnumerable<ClinicDataLayer>> GetClinicFromPersonIdWithPaginationAsync(int personId, int skip, int take);
        Task<IEnumerable<PersonDataLayer>> GetPersonsFromClinicIdWithPaginationAsync(int clinicId, int skip, int take);
        Task DeleteLinkPersonToClinicAsync(int personId, int clinicId);
    }
}
