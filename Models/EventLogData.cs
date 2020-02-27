using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Models
{
    public class EventLogData
    {
        [Key]
        public string id { get; set; }

        public string log { get; set; }
    }
}
