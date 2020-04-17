using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppDomainProject.Models;
using System.ComponentModel.DataAnnotations;

namespace AppDomainProject
{
    public class Retaining_Earnings_StatementModel : ManagerPageModel
    {
        public Retaining_Earnings_StatementModel(AppDomainProjectContext _context) : base(_context) { }

        [BindProperty]
        public List<string> temp { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [BindProperty]
        public double[] array { get; set; }

        public void OnGet(DateTime? date)
        {
            Date = date.HasValue ? date.Value : DateTime.Now;

            temp = new List<string>();
            temp.Add("Beginning Balance");
            temp.Add("Net Income");
            temp.Add("Less Drawings");
            temp.Add("Ending Balance");
            

            double revenue_total = 0;
            double expenses_total = 0;
            double net_total = 0;


            Date = date.HasValue ? date.Value : DateTime.Now;

            revenue_total = 0;
            expenses_total = 0;
            net_total = 0;

            

            var q = from TransactionData a in _context.TransactionData where a.TransactionDate < Date select a;
            Dictionary<string, double> expenses = new Dictionary<string, double>();
            Dictionary<string, double> revenues = new Dictionary<string, double>();

            foreach (TransactionData data in q)
            {
                var query = from m in _context.AccountData select m;
                query = query.Where(n => n.AccountNumber.Equals(data.AccountNumber));
                AccountData Account = query.FirstOrDefault();
                if (Account.AccountCategory.Equals("Revenue") || Account.NormalSide == false)
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
            foreach (var i in revenues)
            {
                revenue_total += i.Value;
            }
            foreach (var i in expenses)
            {
                expenses_total += i.Value;
            }
           
            net_total = revenue_total - expenses_total;
            array = new double[4];
            array[1] = net_total;
        }
        public IActionResult OnPost()
        {
            return Redirect($"./Retaining_Earnings_Statement?date={Date}");
        }
    }
}