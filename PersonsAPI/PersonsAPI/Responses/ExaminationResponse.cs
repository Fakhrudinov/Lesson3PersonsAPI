using System;

namespace PersonsAPI.Responses
{
    public class ExaminationResponse
    {
        public int Id { get; set; }
        public string ProcedureName { get; set; }
        public DateTime ProcedureDate { get; set; }
        public int ProcedureCost { get; set; }

        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }

        public PersonResponse Person { get; set; }
        public int PersonId { get; set; }
        public ClinicResponse Clinic { get; set; }
        public int ClinicId { get; set; }
    }
}
