using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Abstraction.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersonsAsync();
        Task<IEnumerable<Person>> GetPersonsByNameWithPaginationAsync(string searchTerm, int skip, int take);
        Task<IEnumerable<Person>> GetPersonsByNameAsync(string term);
        Task<IEnumerable<Person>> GetPersonsWithPaginationAsync(int skip, int take);
        Task<Person> GetPersonByIdAsync(int id);
        Task DeletePersonByIdAsync(int id);
        Task RegisterPersonAsync(Person newPerson);
        Task EditPersonAsync(Person editPerson, int id);
    }
}
