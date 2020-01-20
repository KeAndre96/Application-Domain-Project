using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Models
{
    public class LoginData
    {
        [Key]
        public string ID { get; set; }

        public string Password { get; set; }
        
        public AccountStatus Status { get; set; }

        public AccountType Class { get; set; }
    }

    public enum AccountStatus
    {
        Active,
        Inactive,
        Suspended
    }

    public enum AccountType
    {
        Admin,
        Manager,
        User
    }
}
