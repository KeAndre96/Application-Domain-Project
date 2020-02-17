using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class EditAccountModel : AdminPageModel
    {
        public EditAccountModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public AccountData Account { get; set; }
        [BindProperty]
        public string Side { get; set; }
        public string[] Sides = new string[] { "Credit", "Debit" };

        public IActionResult OnGet(string acct)
        {
            var query = from m in _context.AccountData select m;
            query = query.Where(n => n.AccountNumber.Equals(acct));

            Account = query.FirstOrDefault();
            if (Account == null)
                return NotFound();
            return Page();
        }

        public IActionResult OnPost()
        {
            if(!string.IsNullOrEmpty(Side))
                Account.NormalSide = Side.Equals(Sides[0]);

            _context.Attach(Account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Redirect($"/User/Account/{Account.AccountNumber}");
        }

        public IActionResult OnPostDeactivate()
        {
            return Redirect($"../DeactivateAccount?acct={Account.AccountNumber}");
        }
    }
}