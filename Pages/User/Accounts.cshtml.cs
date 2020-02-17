using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppDomainProject
{
    public class AccountsModel : AuthenticatedPageModel
    {
        public AccountsModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public List<AccountData> Accounts { get; set; }

        public void OnGet(string? searchString)
        {
            var query = from m in _context.AccountData select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.AccountName.Contains(searchString));
            }
            query = query.Where(n => n.Active);
            Accounts = query.ToList();
      
            //TODO: SORT ACCOUNTS
        }

        public IActionResult OnPost(string acct)
        {
            return Redirect($"./Account/{acct}");
        }
        /*
        public async Task<IActionResult> Search(string searchString)
        {
            var account = from m in _context.AccountData select m;

            if(!String.IsNullOrEmpty(searchString))
            {
                account = account.Where(s => s.AccountName.Contains(searchString));
            }
            return View(await account.ToListAsync());
        }
        */
    }
}