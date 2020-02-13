using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class AccountModel : AuthenticatedPageModel
    {
        public AccountModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public AccountData Account { get; set; }

        public IActionResult OnGet(string acct)
        {
            var query = from m in _context.AccountData select m;
            query = query.Where(n => n.AccountNumber.Equals(acct));
            Account = query.FirstOrDefault();
            if (Account == null)
                return NotFound();
            return Page();
        }

        public string GetSideLabel()
        {
            return Account.NormalSide ? "Credit" : "Debit";
        }

        public string GetCategoryLabel()
        {
            string label = Account.AccountCategory;
            if (!string.IsNullOrEmpty(Account.AccountSubCategory))
            {
                label += '/' + Account.AccountSubCategory;
            }
            return label;
        }
    }
}