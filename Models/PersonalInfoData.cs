using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Models
{
    public class PersonalInfoData
    {
        [Key]
        public string ID { get; set; }

        public string FirtsName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public string Address { get; set; }
    }
}
