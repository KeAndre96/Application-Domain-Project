using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class DashboardModel : AuthenticatedPageModel
    {
        public DashboardModel(AppDomainProjectContext context) : base(context)
        {
        }

        public IActionResult OnGet()
        {
            //TOTO get role and redirect to role's dashboard page
            if(UserInfo.Class == AccountType.User)
                return Redirect("/User");
            if (UserInfo.Class == AccountType.Manager)
                return Redirect("/Manager");
            if (UserInfo.Class == AccountType.Admin)
                return Redirect("/Admin");

            return NotFound();
        }
    }
}