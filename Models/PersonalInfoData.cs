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

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Address { get; set; }
    }
}
