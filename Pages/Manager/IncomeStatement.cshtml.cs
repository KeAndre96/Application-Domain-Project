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
        public void OnGet(DateTime? date)
        {
            Date = date.HasValue ? date.Value : DateTime.Now;

            revenue_total = 0;
            expenses_total = 0;
            net_total = 0;

            Revenue = new List<AccountData>();
            Expenses = new List<AccountData>();

            var q = from AccountData a in _context.AccountData where a.TimeAccountAdded < Date select a;

            foreach(AccountData a in q)
            {
                if(a.Statement == 0)
                {
                    if(a.AccountCategory.Equals("Asset"))
                    {
                        Revenue.Add(a);
                        revenue_total += a.Balance;
                    }
                    else
                    {
                        Expenses.Add(a);
                        expenses_total += a.Balance;
                    }
                }
            }
            net_total = revenue_total - expenses_total;
        }
        public IActionResult OnPost()
        {
            return Redirect($"./IncomeStatement?date={Date}");
        }

    }
}