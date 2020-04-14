using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class IncomeStatementModel : ManagerPageModel
    {
        public IncomeStatementModel(AppDomainProjectContext _context) : base(_context) { }

        [BindProperty]
        public AccountData Account { get; set; }

        [BindProperty]
        public double revenue_total { get; set; }

        [BindProperty]
        public double expenses_total { get; set; }

        [BindProperty]
        public double net_total { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [BindProperty]
        public List<AccountData> Revenue { get; set; }

        [BindProperty]
        public List<AccountData> Expenses { get; set; }

        [BindProperty]
        public Dictionary<string, double> expenses { get; set; }

        [BindProperty]
        public Dictionary<string, double> revenues { get; set; }

        public void OnGet(DateTime? date)
        {
            Date = date.HasValue ? date.Value : DateTime.Now;

            revenue_total = 0;
            expenses_total = 0;
            net_total = 0;

            Revenue = new List<AccountData>();
            Expenses = new List<AccountData>();

            var q = from TransactionData a in _context.TransactionData where a.TransactionDate < Date select a;
            expenses = new Dictionary<string, double>();
            revenues = new Dictionary<string, double>();

            foreach (TransactionData data in q)
            {
                var query = from m in _context.AccountData select m;
                query = query.Where(n => n.AccountNumber.Equals(data.AccountNumber));
                Account = query.FirstOrDefault();
                if (Account.AccountCategory.Equals("Revenue"))
                {
                    if (revenues.ContainsKey(Account.AccountName))
                    {
                        double temp1 = data.Credits + data.Debits;
                        revenues[Account.AccountName] = revenues[Account.AccountName] += temp1;
                    }
                    else
                    {
                        double temp2 = data.Credits + data.Debits;
                        revenues.Add(Account.AccountName, temp2);
                    }
                }
                else
                {
                    if (expenses.ContainsKey(Account.AccountName))
                    {
                        double temp3 = data.Credits + data.Debits;
                        expenses[Account.AccountName] = expenses[Account.AccountName] += temp3;
                    }
                    else
                    {
                        double temp4 = data.Credits + data.Debits;
                        expenses.Add(Account.AccountName, temp4);
                    }
                }
            }
            foreach(var i in revenues)
            {
                revenue_total += i.Value;
            }
            foreach (var i in expenses)
            {
                expenses_total += i.Value;
            }
            net_total = revenue_total - expenses_total;
        }
        public IActionResult OnPost()
        {
            return Redirect($"./IncomeStatement?date={Date}");
        }

    }
}