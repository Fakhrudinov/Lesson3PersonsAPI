using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Abstraction.Entityes
{
    public class PersonDataLayer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public int Age { get; set; }
        public List<ClinicDataLayer> Clinics { get; set; } = new List<ClinicDataLayer>();

        public List<ExaminationDataLayer> Examinations { get; set; } = new List<ExaminationDataLayer>();
    }
}
