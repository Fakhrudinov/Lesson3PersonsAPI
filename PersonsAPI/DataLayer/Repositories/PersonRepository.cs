using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    class PersonRepository : IPersonRepository
    {
        private ApplicationDataContext _context;

        public PersonRepository (ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _context.Persons
                .Where(p => p.Id.Equals(id))
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonsAsync()
        {
            return await _context.Persons.ToArrayAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonsByNameAsync(string term)
        {
            return await _context.Persons
                .Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonsByNameWithPaginationAsync(string term, int skip, int take)
        {
            return await _context.Persons
                .Where(p => p.FirstName.Contains(term) || p.LastName.Contains(term))
                .Skip(skip * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonsWithPaginationAsync(int skip, int take)
        {
            return await _context.Persons
                .Skip(skip * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task DeletePersonByIdAsync(int id)
        {
            Person personToDelete = _context.Persons.Find(id);

            if (personToDelete != null)
            {
                _context.Persons.Remove(personToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RegisterPersonAsync(Person newPerson)
        {
            await _context.Persons.AddAsync(newPerson);
            await _context.SaveChangesAsync();
        }

        public async Task EditPersonAsync(Person editPerson, int id)
        {
            Person personToEdit = _context.Persons.Find(id);

            if (personToEdit != null)
            {
                personToEdit.Age = editPerson.Age;
                personToEdit.Company = editPerson.Company;
                personToEdit.Email = editPerson.Email;
                personToEdit.FirstName = editPerson.FirstName;
                personToEdit.LastName = editPerson.LastName;

                _context.Persons.Update(personToEdit);

                await _context.SaveChangesAsync();
            }
        }
    }
}
