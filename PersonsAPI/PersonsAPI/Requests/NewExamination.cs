using System;
using System.ComponentModel.DataAnnotations;

namespace PersonsAPI.Requests
{
    public class NewExamination
    {
        public string ProcedureName { get; set; }

        public DateTime ProcedureDate { get; set; }

        public int ProcedureCost { get; set; }

        public int PersonId { get; set; }

        public int ClinicId { get; set; }
    }
}
