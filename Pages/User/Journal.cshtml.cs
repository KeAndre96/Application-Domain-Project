using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppDomainProject
{
    public class JournalModel : AuthenticatedPageModel
    {
        public JournalModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public List<TransactionData> Transactions { get; set; } 

        [BindProperty]
        public TransactionData NewTransaction { get; set; }

        public void OnGet(int? journal)
        {
            Transactions = new List<TransactionData>();
            if(journal != null)
            {
                FetchTransactions(journal.Value);
                Transactions.Sort((a, b) =>
                {
                    return (a.Ammount - b.Ammount) < 0 ? 1: -1;
                });
            }
        }

        public string TransactionText(int i)
        {
            return $"{Transactions[i].AccountNumber} - {Transactions[i].Name}: {Transactions[i].Ammount}";
        }

        public IEnumerable<SelectListItem> GetAccounts()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var q = from m in _context.AccountData select m;
            foreach(AccountData acc in q)
            {
                SelectListItem item = new SelectListItem($"{acc.AccountNumber} - {acc.AccountName}", acc.AccountNumber);
                list.Add(item);
            }

            return list;
        }

        private void FetchTransactions(int journal)
        {
            var q1 = from m in _context.JournalData where m.ID == journal select m;
            JournalData dat = q1.FirstOrDefault();
            if (dat != null)
            {
                var q2 = from m in _context.TransactionData where m.Journal == dat.ID select m;
                foreach (TransactionData t in q2)
                {
                    Transactions.Add(t);
                }
            }
        }
    }
}