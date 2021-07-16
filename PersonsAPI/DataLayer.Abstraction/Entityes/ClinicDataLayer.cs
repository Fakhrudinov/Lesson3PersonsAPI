using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Abstraction.Entityes
{
    public class ClinicDataLayer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public List<PersonDataLayer> Persons { get; set; } = new List<PersonDataLayer>();
        //public List<PersonToClinic> PersonToClinics { get; set; } = new List<PersonToClinic>();

    }
}
