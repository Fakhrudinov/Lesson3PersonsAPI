using DataLayer.Abstraction.Entityes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Abstraction.Repositories
{
    public interface IClinicRepository
    {
        Task<IEnumerable<ClinicDataLayer>> GetClinicsAsync();
        Task<IEnumerable<ClinicDataLayer>> GetClinicsByNameWithPaginationAsync(string searchTerm, int skip, int take);
        Task<IEnumerable<ClinicDataLayer>> GetClinicsByNameAsync(string term);
        Task<IEnumerable<ClinicDataLayer>> GetClinicsWithPaginationAsync(int skip, int take);
        Task<ClinicDataLayer> GetClinicByIdAsync(int id);
        Task DeleteClinicByIdAsync(int id);
        Task RegisterClinicAsync(ClinicDataLayer newClinic);
        Task EditClinicAsync(ClinicDataLayer editClinic, int id);
    }
}
