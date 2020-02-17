using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Models
{
    public class AccountData
    {
        [Key]
        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }

        [DisplayName("Account Name")]
        public string AccountName { get; set; }

        [DisplayName("Description")]
        public string AccountDescription { get; set; }

        [DisplayName("Normal Side")]
        public bool NormalSide { get; set; } //true = credit account

        [DisplayName("Category")]
        public string AccountCategory { get; set; }

        [DisplayName("Subcategory")]
        public string AccountSubCategory { get; set; }

        [DisplayName("Initial Balance")]
        public double InitialBalance { get; set; }

        public double Debit { get; set; }

        public double Credit { get; set; }

        public double Balance { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeAccountAdded { get; set; }

        public string ID { get; set; }

        public int Order { get; set; }

        [DisplayName("Statement Type")]
        public AccountStatement Statement { get; set; }

        public string Comment { get; set; }

        public bool Active { get; set; } = true;

        public enum AccountStatement
        {
            IS,
            BS,
            RE
        }

    }
}
