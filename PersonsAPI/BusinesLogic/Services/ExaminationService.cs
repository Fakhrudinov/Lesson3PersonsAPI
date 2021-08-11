using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;
using BusinesLogic.Abstraction.Services;
using DataLayer.Abstraction.Entityes;
using DataLayer.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic.Services
{
    public class ExaminationService : IExaminationService
    {
        private IExaminationRepository _repository;

        public ExaminationService(IExaminationRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateExaminationsAsync(ExaminationToPost examination)
        {
            var newExamination = new ExaminationDataLayer();
            newExamination.ProcedureName = examination.ProcedureName;
            newExamination.ProcedureDate = examination.ProcedureDate;
            newExamination.ProcedureCost = examination.ProcedureCost;
            newExamination.PersonId = examination.PersonId;
            newExamination.ClinicId = examination.ClinicId;

            return await _repository.CreateExaminationAsync(newExamination);
        }

        public async Task<IEnumerable<ExaminationToGet>> GetAllExaminationsAsync()
        {
            var result = await _repository.GetExaminationsAsync();
            return result.Select(c => new ExaminationToGet()
            {
                Id = c.Id,
                ProcedureName = c.ProcedureName,
                ProcedureDate = c.ProcedureDate,
                ProcedureCost = c.ProcedureCost,
                IsPaid = c.IsPaid,
                PaidDate = c.PaidDate,
                PersonId = c.PersonId,
                ClinicId = c.ClinicId
            }).ToArray();
        }

        public async Task<ExaminationToGet> GetExaminationByIdAsync(int id)
        {
            ExaminationDataLayer result = await _repository.GetExaminationByIdAsync(id);

            ExaminationToGet findedExamination = new ExaminationToGet();

            if (result != null)
            {
                findedExamination.Id = result.Id;
                findedExamination.ProcedureName = result.ProcedureName;
                findedExamination.ProcedureDate = result.ProcedureDate;
                findedExamination.ProcedureCost = result.ProcedureCost;
                findedExamination.IsPaid = result.IsPaid;
                findedExamination.PaidDate = result.PaidDate;
                findedExamination.PersonId = result.PersonId;
                findedExamination.ClinicId = result.ClinicId;
            }
            return findedExamination;
        }

        public async Task<IEnumerable<ExaminationToGet>> GetExaminationsByClientIdAsync(int id, bool isPaid)
        {
            IEnumerable<ExaminationDataLayer> result = await _repository.GetExaminationsByClientIdAsync(id, isPaid);
            return result.Select(c => new ExaminationToGet()
            {
                Id = c.Id,
                ProcedureName = c.ProcedureName,
                ProcedureDate = c.ProcedureDate,
                ProcedureCost = c.ProcedureCost,
                IsPaid = c.IsPaid,
                PaidDate = c.PaidDate,
                PersonId = c.PersonId,
                ClinicId = c.ClinicId
            }).ToArray();
        }

        public async Task<IEnumerable<ExaminationToGet>> GetExaminationsByClinicIdAsync(int id, bool isPaid)
        {
            IEnumerable<ExaminationDataLayer> result = await _repository.GetExaminationsByClinicIdAsync(id, isPaid);
            return result.Select(c => new ExaminationToGet()
            {
                Id = c.Id,
                ProcedureName = c.ProcedureName,
                ProcedureDate = c.ProcedureDate,
                ProcedureCost = c.ProcedureCost,
                IsPaid = c.IsPaid,
                PaidDate = c.PaidDate,
                PersonId = c.PersonId,
                ClinicId = c.ClinicId
            }).ToArray();
        }

        public async Task<IEnumerable<ExaminationToGet>> GetExaminationsFromDateByClinicIdAsync(int id, DateTime date)
        {
            IEnumerable<ExaminationDataLayer> result = await _repository.GetExaminationsFromDateByClinicIdAsync(id, date);
            return result.Select(c => new ExaminationToGet()
            {
                Id = c.Id,
                ProcedureName = c.ProcedureName,
                ProcedureDate = c.ProcedureDate,
                ProcedureCost = c.ProcedureCost,
                IsPaid = c.IsPaid,
                PaidDate = c.PaidDate,
                PersonId = c.PersonId,
                ClinicId = c.ClinicId
            }).ToArray();
        }

        public async Task<IEnumerable<int>> GetFreeExaminationsByClinicIdAsync(int id, DateTime date)
        {
            date = date.Date;

            //получим все записи с клиники за дату
            IEnumerable<ExaminationDataLayer> result = await _repository.GetExaminationsFromDateByClinicIdAsync(id, date);

            List<int> freeHours = new List<int> { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23 };

            foreach (ExaminationDataLayer exam in result)
            {
                foreach (int hour in freeHours)
                {
                    if (exam.ProcedureDate.Hour == hour)
                    {
                        freeHours.Remove(hour);
                        break;
                    }
                }
            }

            return freeHours;
        }

        public async Task SetPaimentToExaminationIdAsync(int id)
        {
            ExaminationDataLayer examination = await _repository.GetExaminationByIdAsync(id);

            if(examination != null)
            {
                var newExamination = new ExaminationToPost(
                    examination.ProcedureName,
                    examination.ProcedureDate,
                    examination.ProcedureCost,
                    examination.PersonId,
                    examination.ClinicId);

                newExamination.SetPaiment();

                examination.IsPaid = newExamination.IsPaid;
                examination.PaidDate = newExamination.PaidDate;

                await _repository.SetPaimentToExaminationIdAsync(examination, id);
            }
        }
    }
}
