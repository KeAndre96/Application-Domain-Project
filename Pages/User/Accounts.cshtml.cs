using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class AccountsModel : AuthenticatedPageModel
    {
        public AccountsModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public List<AccountData> Accounts { get; set; }

        public void OnGet()
        {
            var query = from m in _context.AccountData select m;
            query = query.Where(n => n.Active);
            Accounts = query.ToList();
            //TODO: SORT ACCOUNTS
        }

        public IActionResult OnPost(string acct)
        {
            return Redirect($"./Account/{acct}");
        }
    }
}