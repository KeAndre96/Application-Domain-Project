using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Models
{
    public class JournalData
    {

        [Key]
        public int ID { get; set; }

        public string usetID { get; set; }

        public Status JournalStatus { get; set; }

        public enum Status { incomplete, pending, approved, rejected }
    }
}
