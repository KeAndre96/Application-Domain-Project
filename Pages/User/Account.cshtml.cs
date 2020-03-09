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
    public class AccountModel : AuthenticatedPageModel
    {
        public AccountModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public AccountData Account { get; set; }

        [BindProperty]
        public List<TransactionData> AccountTransactions { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime TransactionStartDate { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime TransactionEndDate { get; set; }

        [DataType(DataType.Currency)]
        [BindProperty]
        public double Cashflow { get; set; }

        [BindProperty]
        public string acct { get; set; }

        public IActionResult OnGet(string acct, DateTime? begin, DateTime? end)
        {
            var query = from m in _context.AccountData select m;
            query = query.Where(n => n.AccountNumber.Equals(acct));
            Account = query.FirstOrDefault();
            if (Account == null)
                return NotFound();
            DateTime trueBegin = begin.HasValue ? begin.Value : DateTime.MinValue;
            DateTime trueEnd = end.HasValue ? end.Value : DateTime.MaxValue;

            AccountTransactions = GetTransactions(trueBegin, trueEnd);

            List<TransactionData> allTransactions;
            if (trueBegin <= Account.TimeAccountAdded && trueEnd >= DateTime.Now)
                allTransactions = AccountTransactions;
            else
                allTransactions = GetTransactions(DateTime.MinValue, DateTime.MaxValue);

            Account.Balance = Account.TimeAccountAdded >= trueBegin ? Account.InitialBalance : 0;
            foreach (TransactionData t in allTransactions)
            {
                Account.Balance += t.Ammount;
            }

            Cashflow = 0;
            foreach(TransactionData t in AccountTransactions)
            {
                Cashflow += t.Ammount;
            }

            TransactionStartDate = begin.HasValue ? begin.Value : Account.TimeAccountAdded;
            TransactionEndDate = end.HasValue ? end.Value : DateTime.Now;
            
            acct = Account.AccountNumber;

            return Page();
        }

        public IActionResult OnPost()
        {
            return Redirect($"./{acct}?begin={TransactionStartDate}&end={TransactionEndDate}");
        }

        public string GetSideLabel()
        {
            return Account.NormalSide ? "Credit" : "Debit";
        }

        public string GetCategoryLabel()
        {
            string label = Account.AccountCategory;
            if (!string.IsNullOrEmpty(Account.AccountSubCategory))
            {
                label += '/' + Account.AccountSubCategory;
            }
            return label;
        }

        public bool UserIsAdmin()
        {
            return UserInfo.Class == AccountType.Admin;
        }

        private List<TransactionData> GetTransactions(DateTime start, DateTime end)
        {
            var q = from t in _context.TransactionData join j in _context.JournalData on t.Journal equals j.ID where j.JournalStatus == JournalData.Status.approved select t;
            q = q.Where(t => t.TransactionDate >= start);
            q = q.Where(t => t.TransactionDate <= end);
            List<TransactionData> list = q.ToList();
            list.Sort((a, b) => { return a.TransactionDate.CompareTo(b.TransactionDate); });
            return list;
        }
    }
}