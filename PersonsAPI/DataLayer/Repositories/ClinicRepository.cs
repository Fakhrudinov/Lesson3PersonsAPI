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

        public async Task RegisterClinicAsync(ClinicDataLayer newClinic)
        {
            await _context.Clinics.AddAsync(newClinic);
            await _context.SaveChangesAsync();
        }
    }
}
