using PersonsAPI.Requests;
using System;

namespace BusinesLogic.Abstraction.Requests
{
    public class ExaminationToPost
    {
        public ExaminationToPost(string procedureName, DateTime procedureDate, int procedureCost, int personId, int clinicId)
        {
            ProcedureName = procedureName;
            ProcedureDate = procedureDate;
            ProcedureCost = procedureCost;
            PersonId = personId;
            ClinicId = clinicId;
        }

        public int Id { get; }
        public string ProcedureName { get; }
        public DateTime ProcedureDate { get; }
        public int ProcedureCost { get; }

        public bool IsPaid { get; private set; }
        public DateTime PaidDate { get; private set; }

        public int PersonId { get; }
        public int ClinicId { get; }

        public void SetPaiment()
        {
            if (IsPaid)
                throw new InvalidOperationException();

            IsPaid = true;
            PaidDate = DateTime.Now;
        }
    }
}
