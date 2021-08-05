using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Abstraction.Entityes;
using DataLayer.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DataLayer.Repositories
{
    public class ExaminationRepository : IExaminationRepository
    {
        private ApplicationDataContext _context;

        public ExaminationRepository(ApplicationDataContext context)
        {
            _context = context;
        }

        public async Task<int> CreateExaminationAsync(ExaminationDataLayer examination)
        {

            //var person = (await _context.Persons.Where(p => p.Id.Equals(examination.PersonId)).SingleOrDefaultAsync());
            //var clinic = (await _context.Clinics.Where(p => p.Id.Equals(examination.ClinicId)).SingleOrDefaultAsync());

            //_context.Clinics.Update(clinic).Entity.Examinations.Add(examination);
            //_context.Persons.Update(person).Entity.Examinations.Add(examination);

            await _context.Examinations.AddAsync(examination);

            await _context.SaveChangesAsync();

            return examination.Id;
        }

        public async Task<ExaminationDataLayer> GetExaminationByIdAsync(int id)
        {
            return await _context.Examinations
                .Where(c => c.Id.Equals(id))
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ExaminationDataLayer>> GetExaminationsAsync()
        {
            return await _context.Examinations.ToArrayAsync();
        }

        public async Task<IEnumerable<ExaminationDataLayer>> GetExaminationsByClientIdAsync(int id, bool isPaid)
        {            
            return await _context.Examinations
                .Where(p => p.PersonId == id && p.IsPaid == isPaid)
                .ToArrayAsync();            
        }

        public async Task<IEnumerable<ExaminationDataLayer>> GetExaminationsByClinicIdAsync(int id, bool isPaid)
        {
            return await _context.Examinations
                .Where(p => p.ClinicId == id && p.IsPaid == isPaid)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<ExaminationDataLayer>> GetExaminationsFromDateByClinicIdAsync(int id, DateTime date)
        {
            return await _context.Examinations
                .Where(p => p.ClinicId == id && (p.ProcedureDate > date && p.ProcedureDate < date.AddDays(1)))
                .ToArrayAsync();
        }

        public async Task SetPaimentToExaminationIdAsync(ExaminationDataLayer newExamination, int id)
        {
            ExaminationDataLayer examinationToEdit = _context.Examinations.Find(id);

            if (examinationToEdit != null)
            {
                examinationToEdit.IsPaid = newExamination.IsPaid;
                examinationToEdit.PaidDate = newExamination.PaidDate;

                _context.Examinations.Update(examinationToEdit);

                await _context.SaveChangesAsync();
            }
        }
    }
}
