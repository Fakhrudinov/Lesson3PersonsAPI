using System;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.Abstraction.Entityes
{
    public class ExaminationDataLayer
    {
        [Key]
        public int Id { get; set; }
        public string ProcedureName { get; set; }
        public DateTime ProcedureDate { get; set; }
        public int ProcedureCost { get; set; }

        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }

        public PersonDataLayer Person { get; set; }
        public int PersonId { get; set; }
        public ClinicDataLayer Clinic { get; set; }
        public int ClinicId { get; set; }  
    }
}
