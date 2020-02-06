using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Models
{
    public class AccountData
    {
        [Key]
        public string AccountNumber { get; set; }

        public string AccountName { get; set; }

        public string AccountDescription { get; set; }

        public string NormalSide { get; set; }

        public string AccountCategory { get; set; }

        public string AccountSubCategory { get; set; }

    }
}
