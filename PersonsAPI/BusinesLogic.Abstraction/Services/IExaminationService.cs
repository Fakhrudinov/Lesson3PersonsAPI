using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinesLogic.Abstraction.Services
{
    public interface IExaminationService
    {
        Task<IEnumerable<ExaminationToGet>> GetAllExaminationsAsync();
        Task<int> CreateExaminationsAsync(ExaminationToPost examination);
        Task SetPaimentToExaminationIdAsync(int id);
        Task<ExaminationToGet> GetExaminationByIdAsync(int id);
        Task<IEnumerable<ExaminationToGet>> GetExaminationsByClientIdAsync(int id, bool isPaid);
        Task<IEnumerable<ExaminationToGet>> GetExaminationsByClinicIdAsync(int id, bool isPaid);
        Task<IEnumerable<int>> GetFreeExaminationsByClinicIdAsync(int id, DateTime date);
        Task<IEnumerable<ExaminationToGet>> GetExaminationsFromDateByClinicIdAsync(int id, DateTime date);
    }
}
