using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Models
{
    public class PasswordData
    {
        [Key]
        public string ID { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string SecurityAnswer1 { get; set; }

        public string SecurityAnswer2 { get; set; }

        public string SecurityAnswer3 { get; set; }
    }

    
}
