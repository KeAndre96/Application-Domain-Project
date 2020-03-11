using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
            Account.Active = true;

            _context.Attach(Account).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();

            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            string id = r.Next().ToString();
            var ans = _context.EventLogData.Find(id);
            if (ans == null)
            {
                DateTime localDate = DateTime.Now;
                sb.Append(UserInfo.ID + " changed added to Chart Accounts: " + localDate);
                EventLogData temp = new EventLogData { id = id, log = sb.ToString(), before_image = "", after_image = JsonConvert.SerializeObject(Account) };
                _context.EventLogData.Add(temp);
            }
            else
            {
                while (ans != null)
                {
                    id = r.Next().ToString();
                    ans = _context.EventLogData.Find(id);
                }
                DateTime localDate = DateTime.Now;
                sb.Append(UserInfo.ID + " added to Chart of Accounts: " + localDate);
                EventLogData temp = new EventLogData { id = id, log = sb.ToString(), before_image = "", after_image = JsonConvert.SerializeObject(Account) };

                _context.EventLogData.Add(temp);
            }
            _context.SaveChanges();



            return Redirect("/User/Accounts");
        }
    }
}