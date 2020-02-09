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

        public double InitialBalence { get; set; }

        public string Debit { get; set; }

        public string Credit { get; set; }

        public double Balence { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeAccountAdded { get; set; }

        public string UserId { get; set; }

        public int Order { get; set; }

        public AccountStatement Statement { get; set; }

        public string Comment { get; set; }

        public enum AccountStatement
        {
            IS,
            BS,
            RE
        }

    }
}
