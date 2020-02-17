using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class DeactivateAccountModel : AdminPageModel
    {
        public DeactivateAccountModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public AccountData Account{ get; set; }

        [BindProperty]
        public bool Allowed { get; set; }

        public IActionResult OnGet(string acct)
        {
            var query = from m in _context.AccountData select m;
            query = query.Where(n => n.AccountNumber.Equals(acct));

            Account = query.FirstOrDefault();
            if (Account == null)
                return NotFound();

            Allowed = (Account.Balance == 0);

            return Page();
        }

        public IActionResult OnPostConfirm(string acct)
        {
            var query = from m in _context.AccountData select m;
            query = query.Where(n => n.AccountNumber.Equals(acct));
            AccountData account = query.FirstOrDefault();
            if (account == null)
                return BadRequest();

            account.Active = false;
            _context.Attach(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Redirect("/User/Accounts");
        }

        public IActionResult OnPostCancel(string acct)
        {
            return Redirect($"./EditAccount/{acct}");
        }
    }
}