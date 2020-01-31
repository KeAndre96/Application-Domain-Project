using AppDomainProject.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject.Authorization
{
    public class UserAuthorizationRequirement : IAuthorizationRequirement
    {
        public AccountType? AccountType { get; }
        public UserAuthorizationRequirement(AccountType? Type = null)
        {
            AccountType = Type;
        }
    }
}
