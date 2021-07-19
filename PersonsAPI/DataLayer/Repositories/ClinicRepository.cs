using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using DataLayer.Abstraction.Entityes;

namespace DataLayer.Repositories
{
    class ClinicRepository : IClinicRepository
    {
        private ApplicationDataContext _context;

        public ClinicRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<ClinicDataLayer> GetClinicByIdAsync(int id)
        {
            return await _context.Clinics
                .Where(c => c.Id.Equals(id))
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ClinicDataLayer>> GetClinicsAsync()
        {
            return await _context.Clinics.ToArrayAsync();
        }

        public async Task<IEnumerable<ClinicDataLayer>> GetClinicsByNameAsync(string term)
        {
            return await _context.Clinics
                .Where(p => p.Name.Contains(term))
                .ToArrayAsync();
        }

        public async Task<IEnumerable<ClinicDataLayer>> GetClinicsByNameWithPaginationAsync(string term, int skip, int take)
        {
            return await _context.Clinics
                .Where(p => p.Name.Contains(term))
                .Skip(skip * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<ClinicDataLayer>> GetClinicsWithPaginationAsync(int skip, int take)
        {
            return await _context.Clinics
                .Skip(skip * take)
                .Take(take)
                .ToArrayAsync();
        }

        public async Task RegisterClinicAsync(ClinicDataLayer newClinic)
        {
            await _context.Clinics.AddAsync(newClinic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClinicByIdAsync(int id)
        {
            ClinicDataLayer clinicToDelete = _context.Clinics.Find(id);

            if (clinicToDelete != null)
            {
                _context.Clinics.Remove(clinicToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditClinicAsync(ClinicDataLayer editClinic, int id)
        {
            ClinicDataLayer clinicToEdit = _context.Clinics.Find(id);

            if (clinicToEdit != null)
            {
                clinicToEdit.Name = editClinic.Name;
                clinicToEdit.Adress = editClinic.Adress;

                _context.Clinics.Update(clinicToEdit);

                await _context.SaveChangesAsync();
            }
        }
    }
}
