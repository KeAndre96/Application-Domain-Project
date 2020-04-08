using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AppDomainProject.Pages.Manager
{
    public class BalanceSheetModel : ManagerPageModel
    {

        public BalanceSheetModel(AppDomainProjectContext _context) : base(_context) { }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [BindProperty]
        public List<AccountData> AssetAccounts { get; set; }

        [BindProperty]
        public List<AccountData> LiabilityAccounts { get; set; }

        [BindProperty]
        public double TotalAssets { get; set; }

        [BindProperty]
        public double TotalLiabilities { get; set; }

        public void OnGet(DateTime? date)
        {
            Date = date.HasValue ? date.Value : DateTime.Now;

            AssetAccounts = new List<AccountData>();
            LiabilityAccounts = new List<AccountData>();

            var q = from AccountData a in _context.AccountData where a.TimeAccountAdded < Date select a;
            foreach(AccountData a in q)
            {
                a.Balance = a.InitialBalance;

                //calculate balance
                var q2 = from TransactionData t in _context.TransactionData
                         join JournalData j in _context.JournalData on t.Journal equals j.ID
                         where j.JournalStatus == JournalData.Status.approved
                         where t.TransactionDate < Date
                         where t.AccountNumber == a.AccountNumber
                         select t;
                foreach(TransactionData t in q2)
                {
                    if (a.NormalSide)
                    {
                        a.Balance += t.Credits;
                        a.Balance -= t.Debits;
                    }
                    else
                    {
                        a.Balance -= t.Credits;
                        a.Balance += t.Debits;
                    }
                }
                if (a.NormalSide)
                {
                    LiabilityAccounts.Add(a);
                    TotalLiabilities += a.Balance;
                }
                else
                {
                    AssetAccounts.Add(a);
                    TotalAssets += a.Balance;
                }
            }
        }

        public IActionResult OnPost()
        {
            return Redirect($"./BalanceSheet?date={Date}");
        }
    }
}