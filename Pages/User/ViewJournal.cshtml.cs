using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppDomainProject.Pages.User
{
    public class ViewJournalModel : AuthenticatedPageModel
    {
        public ViewJournalModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public List<TransactionData> Transactions { get; set; }

        [BindProperty]
        public int? Journal { get; set; }

        [BindProperty]
        public JournalData JournalData { get; set; }

        public IActionResult OnGet(int? journal)
        {
            Transactions = new List<TransactionData>();
            if (journal != null)
            {
                FetchTransactions(journal.Value);
                Transactions.Sort((a, b) =>
                {
                    return (a.Ammount - b.Ammount) < 0 ? 1 : -1;
                });

                var q = from m in _context.JournalData where m.ID == journal.Value select m;
                JournalData = q.FirstOrDefault();
                if (JournalData == null)
                    return NotFound();

                return Page();
            }
            return NotFound();
        }

        public IActionResult OnPostApprove(int journal)
        {
            JournalData j = (from m in _context.JournalData where m.ID == journal select m).FirstOrDefault();
            if (j == null)
                return NotFound();

            j.JournalStatus = JournalData.Status.approved;
            _context.Attach(j).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Redirect($"./{journal}");
        }

        public IActionResult OnPostReject(int journal)
        {
            JournalData j = (from m in _context.JournalData where m.ID == journal select m).FirstOrDefault();
            if (j == null)
                return NotFound();

            j.JournalStatus = JournalData.Status.rejected;
            _context.Attach(j).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Redirect($"./{journal}");
        }

        public bool CanApprove()
        {
            return UserInfo.Class == AccountType.Manager;
        }

        public string JournalStatusTag()
        {
            switch (JournalData.JournalStatus)
            {
                case JournalData.Status.pending: return "- Pending";
                case JournalData.Status.rejected: return "- Rejected";
                case JournalData.Status.incomplete: return "- In Progress";
                default: return "";
            }
        }

        public JournalEntry JournalLine(int i)
        {
            return new JournalEntry
            {
                TransactionName = Transactions[i].Name,
                Ammount = Transactions[i].Ammount,
                Desc = Transactions[i].Description,
                AccountName = (from m in _context.AccountData where m.AccountNumber == Transactions[i].AccountNumber select m.AccountName).FirstOrDefault()
            };
        }

        public string JournalLabelText()
        {
            return Journal == null ? "New Journal" : $"Journal - {Journal.Value}";
        }

        public IEnumerable<SelectListItem> GetAccounts()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var q = from m in _context.AccountData select m;
            foreach (AccountData acc in q)
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

        public class JournalEntry
        {
            public string AccountName { get; set; }
            public string TransactionName { get; set; }
            public double Ammount { get; set; }
            public string Desc { get; set; }
        }
    }
}
