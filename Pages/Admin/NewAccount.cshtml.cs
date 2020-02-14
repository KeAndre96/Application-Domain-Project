using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class NewAccountModel : AdminPageModel
    {
        public NewAccountModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public AccountData Account { get; set; }

        [BindProperty]
        public string Side { get; set; }
        public string[] Sides = new string[] { "Credit", "Debit" };

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            Account.TimeAccountAdded = DateTime.Now;
            Account.ID = UserInfo.ID;
            Account.Comment = "";
            Account.Credit = 0;
            Account.Debit = 0;
            Account.Balance = Account.InitialBalance;

            Account.NormalSide = Side.Equals(Sides[0]);

            _context.Attach(Account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();

            return Redirect("/User/Accounts");
        }
    }
}