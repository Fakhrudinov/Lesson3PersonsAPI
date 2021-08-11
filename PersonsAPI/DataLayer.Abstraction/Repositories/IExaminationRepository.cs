using DataLayer.Abstraction.Entityes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Abstraction.Repositories
{
    public interface IExaminationRepository
    {
        Task<IEnumerable<ExaminationDataLayer>> GetExaminationsAsync();
        Task<int> CreateExaminationAsync(ExaminationDataLayer examination);
        Task<ExaminationDataLayer> GetExaminationByIdAsync(int id);
        Task SetPaimentToExaminationIdAsync(ExaminationDataLayer newExamination, int id);
        Task<IEnumerable<ExaminationDataLayer>> GetExaminationsByClientIdAsync(int id, bool isPaid);
        Task<IEnumerable<ExaminationDataLayer>> GetExaminationsByClinicIdAsync(int id, bool isPaid);
        Task<IEnumerable<ExaminationDataLayer>> GetExaminationsFromDateByClinicIdAsync(int id, DateTime date);
    }
}
