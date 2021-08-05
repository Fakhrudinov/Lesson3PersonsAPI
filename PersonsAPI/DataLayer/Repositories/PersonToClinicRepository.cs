using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Abstraction.Entityes;
using DataLayer.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DataLayer.Repositories
{
    public class PersonToClinicRepository : IPersonToClinicRepository
    {
        private ApplicationDataContext _context;

        public PersonToClinicRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task SetClinicToPersonByIDAsync(int personId, int clinicId)
        {
            var person = (await _context.Persons.Where(p => p.Id.Equals(personId)).SingleOrDefaultAsync());
            var clinic = (await _context.Clinics.Where(p => p.Id.Equals(clinicId)).SingleOrDefaultAsync());

            if (person != null && clinic != null)
            {
                _context.Persons.Update(person).Entity.Clinics.Add(clinic);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ClinicDataLayer>> GetClinicFromPersonIdAsync(int personId)
        {
            // первый способ
            //var linkedPerson = (await _context.Persons
            //    .Include(c => c.Clinics)
            //    .Where(p => p.Id.Equals(personId))
            //    .SingleOrDefaultAsync());

            //var result = new List<ClinicDataLayer>();
            //foreach (ClinicDataLayer clinic in linkedPerson.Clinics)
            //{
            //    result.Add(clinic);
            //}

            //второй способ
            var result = (await _context.Clinics
                .Include(c => c.Persons)
                .Where(c => c.Persons.Any(c => c.Id == personId))
                .ToArrayAsync());

            return result;
        }

        public async Task<IEnumerable<PersonDataLayer>> GetPersonsFromClinicIdAsync(int clinicId)
        {
            // первый способ
            //var linkedClinics = (await _context.Clinics
            //    .Include(p => p.Persons)
            //    .Where(c => c.Id.Equals(clinicId))
            //    .SingleOrDefaultAsync());

            //var result = new List<PersonDataLayer>();
            //foreach (PersonDataLayer person in linkedClinics.Persons)
            //{
            //    result.Add(person);
            //}

            //второй способ
            var result = (await _context.Persons
                .Include(c => c.Clinics)
                .Where(c => c.Clinics.Any(c => c.Id == clinicId))
                .ToArrayAsync());

            return result;
        }

        public async Task<IEnumerable<ClinicDataLayer>> GetClinicFromPersonIdWithPaginationAsync(int personId, int skip, int take)
        {
            var result = (await _context.Clinics
                .Include(c => c.Persons)
                .Where(c => c.Persons.Any(c => c.Id == personId))
                .Skip(skip * take)
                .Take(take)
                .ToArrayAsync());

            return result;
        }

        public async Task<IEnumerable<PersonDataLayer>> GetPersonsFromClinicIdWithPaginationAsync(int clinicId, int skip, int take)
        {
            var result = (await _context.Persons
                .Include(c => c.Clinics)
                .Where(c => c.Clinics.Any(c => c.Id == clinicId))
                .Skip(skip * take)
                .Take(take)
                .ToArrayAsync());

            return result;
        }

        public async Task DeleteLinkPersonToClinicAsync(int personId, int clinicId)
        {
            var person = (await _context.Persons.Where(p => p.Id.Equals(personId)).SingleOrDefaultAsync());
            var clinic = (await _context.Clinics.Where(p => p.Id.Equals(clinicId)).SingleOrDefaultAsync());

            if (person != null && clinic != null)
            {
                var result = (await _context.Persons
                    .Include(c => c.Clinics)
                    .Where(c => c.Clinics.Any(c => c.Id == clinicId))
                    .ToArrayAsync());

                _context.Persons.Update(person).Entity.Clinics.Remove(clinic);

                await _context.SaveChangesAsync();
            }
        }
    }
}
