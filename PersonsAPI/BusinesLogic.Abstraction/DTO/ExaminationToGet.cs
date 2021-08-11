using System;

namespace BusinesLogic.Abstraction.DTO
{
    public class ExaminationToGet
    {
        public int Id { get; set; }
        public string ProcedureName { get; set; }
        public DateTime ProcedureDate { get; set; }
        public int ProcedureCost { get; set; }

        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }

        public PersonToGet Person { get; set; }
        public int PersonId { get; set; }
        public ClinicToGet Clinic { get; set; }
        public int ClinicId { get; set; }
    }
}
