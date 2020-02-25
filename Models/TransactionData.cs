using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Models
{
    public class TransactionData
    {
        [Key]
        public int ID { get; set; }

        public int Journal { get; set; }

        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public double Ammount { get; set; }

        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
    }
}
