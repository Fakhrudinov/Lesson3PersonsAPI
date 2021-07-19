using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinesLogic.Abstraction.Services
{
    public interface IClinicService
    {
        Task<IEnumerable<ClinicToGet>> GetClinicsAsync();
        Task<IEnumerable<ClinicToGet>> GetClinicsByNameAsync(string term);
        Task<ClinicToGet> GetClinicByIdAsync(int id);
        Task<IEnumerable<ClinicToGet>> GetClinicsByNameWithPaginationAsync(string searchTerm, int skip, int take);
        Task<IEnumerable<ClinicToGet>> GetClinicsWithPaginationAsync(int skip, int take);
        Task RegisterClinicAsync(ClinicToPost newClinic);
        Task EditClinicAsync(ClinicToGet newClinic, int id);
        Task DeleteClinicByIdAsync(int id);
    }
}
