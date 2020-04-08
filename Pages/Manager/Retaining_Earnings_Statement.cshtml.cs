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


            var q = from AccountData a in _context.AccountData where a.TimeAccountAdded < Date select a;

            foreach (AccountData a in q)
            {
                if (a.Statement == 0)
                {
                    if (a.AccountCategory.Equals("Asset"))
                    {
                        revenue_total += a.Balance;
                    }
                    else
                    {
                        expenses_total += a.Balance;
                    }
                }
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