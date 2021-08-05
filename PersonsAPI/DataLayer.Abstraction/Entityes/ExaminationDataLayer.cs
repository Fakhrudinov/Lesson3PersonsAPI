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

        //public ExaminationDataLayer(string procedureName, DateTime procedureDate, int procedureCost, int personId, int clinicId)
        //{
        //    ProcedureName = procedureName;
        //    ProcedureDate = procedureDate;
        //    ProcedureCost = procedureCost;
        //    PersonId = personId;
        //    ClinicId = clinicId;
        //}

        //[Key]
        //public int Id { get; }
        //public string ProcedureName { get; }
        //public DateTime ProcedureDate { get; }
        //public int ProcedureCost { get; }

        //public bool IsPaid { get; private set; }
        //public DateTime PaidDate { get; private set; }

        //public PersonDataLayer Person { get; set; }
        //public int PersonId { get; }
        //public ClinicDataLayer Clinic { get; set; }
        //public int ClinicId { get; }


        //public void SetPaiment()
        //{
        //    if (IsPaid)
        //        throw new InvalidOperationException();

        //    IsPaid = true;
        //    PaidDate = DateTime.Now;
        //}

    }
}
