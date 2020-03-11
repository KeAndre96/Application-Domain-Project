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
using Newtonsoft.Json;

namespace AppDomainProject
{
    public class EditAccountModel : AdminPageModel
    {
        public EventLogData first = new EventLogData { };
        public EditAccountModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public AccountData Account { get; set; }
        [BindProperty]
        public string Side { get; set; }
        public string[] Sides = new string[] { "Credit", "Debit" };
        [BindProperty]
        public string before_image { get; set; }
        
        public string test;
        
        public IActionResult OnGet(string acct)
        {
            var query = from m in _context.AccountData select m;
            query = query.Where(n => n.AccountNumber.Equals(acct));

            Account = query.FirstOrDefault();

            before_image = JsonConvert.SerializeObject(Account);
        

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
                if(e.State.Equals(Microsoft.EntityFrameworkCore.EntityState.Modified))
                {
                    columnName = e.Entity.ToString();
                }
            }

            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            string id = r.Next().ToString();
            var ans = _context.EventLogData.Find(id);
            if(ans == null)
            {
                DateTime localDate = DateTime.Now;
                sb.Append(UserInfo.ID + " changed something in Chart of Accounts: " + localDate);
                EventLogData temp = new EventLogData { id = id, log = sb.ToString(), before_image = before_image, after_image = JsonConvert.SerializeObject(Account) };
                _context.EventLogData.Add(temp);
            }
            else
            {
                while(ans != null)
                {
                    id = r.Next().ToString();
                    ans = _context.EventLogData.Find(id);
                }
                DateTime localDate = DateTime.Now;
                sb.Append(UserInfo.ID + " changed something in Chart of Accounts: " + localDate);
                EventLogData temp = new EventLogData { id = id, log = sb.ToString(), before_image = before_image, after_image = JsonConvert.SerializeObject(Account) };
                
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
//json format for account data
///"{\"AccountNumber\":\"\",\"AccountName\":\"\",\"AccountDescription\":\"\",\"NormalSide\":true,\"AccountCategory\":\"\",\"AccountSubCategory\":\"\",\"InitialBalance\":0.0,\"Debit\":0.0,\"Credit\":0.0,\"Balance\":0.0,\"TimeAccountAdded\":\"0000-00-00T00:00:00\",\"ID\":\"\",\"Order\":1,\"Statement\":0,\"Comment\":null,\"Active\":true}"