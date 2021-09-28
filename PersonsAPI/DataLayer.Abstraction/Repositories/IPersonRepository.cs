using DataLayer.Abstraction.Entityes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Abstraction.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<PersonDataLayer>> GetPersonsAsync();
        Task<IEnumerable<PersonDataLayer>> GetPersonsByNameWithPaginationAsync(string searchTerm, int skip, int take);
        Task<IEnumerable<PersonDataLayer>> GetPersonsByNameAsync(string term);
        Task<IEnumerable<PersonDataLayer>> GetPersonsWithPaginationAsync(int skip, int take);
        Task<PersonDataLayer> GetPersonByIdAsync(int id);
        Task DeletePersonByIdAsync(int id);
        Task RegisterPersonAsync(PersonDataLayer newPerson);
        Task EditPersonAsync(PersonDataLayer editPerson, int id);
    }
}
