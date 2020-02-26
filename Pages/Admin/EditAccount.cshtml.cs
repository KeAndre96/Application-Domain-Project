using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            if (!string.IsNullOrEmpty(Side))
            {
                Account.NormalSide = Side.Equals(Sides[0]);
                //sb.Append();
                //EventLogData temp = new EventLogData { id=}
            }

            _context.Attach(Account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            string columnName = "";
            foreach(EntityEntry e in _context.ChangeTracker.Entries())
            {
                if(e.State.Equals("Modified"))
                {
                    columnName = e.Entity.ToString();
                }
            }
            StringBuilder sb = new StringBuilder();
            //string id = UserInfo.ID;
            Random r = new Random();
            string id = r.Next().ToString();
            var ans = _context.EventLogData.Find(id);
            if(ans == null)
            {
                sb.Append(UserInfo.ID + " changed something in Chart of Accounts");
                EventLogData temp = new EventLogData { id = id, log = sb.ToString() };
                _context.EventLogData.Add(temp);
            }
            else
            {
                while(ans != null)
                {
                    id = r.Next().ToString();
                    ans = _context.EventLogData.Find(id);
                }
                sb.Append(UserInfo.ID + " changed something in Chart of Accounts");
                EventLogData temp = new EventLogData { id = id, log = sb.ToString() };
                _context.EventLogData.Add(temp);
            }
            _context.SaveChanges();

            return Redirect($"/User/Account/{Account.AccountNumber}");
        }

        public IActionResult OnPostDeactivate()
        {
            return Redirect($"../DeactivateAccount?acct={Account.AccountNumber}");
        }
    }
}