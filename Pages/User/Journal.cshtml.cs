﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [BindProperty]
        public int? Journal { get; set; }

        public List<JournalData> Journals { get; set; }

        public void OnGet(int? journal, string? searchString)
        {
            var query = from m in _context.JournalData select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.usetID.Contains(searchString));
            }
            //query = query.Where(n => n.Active);
            Journals = query.ToList();

            Transactions = new List<TransactionData>();
            if(journal != null)
            {
                FetchTransactions(journal.Value);
                Transactions.Sort((a, b) =>
                {
                    return ((a.Credits - a.Debits) - (b.Credits - b.Debits)) < 0 ? 1 : -1;
                });
            }
            

            Journal = journal;
            NewTransaction = new TransactionData
            {
                TransactionDate = DateTime.Now
            };
        }

        public IActionResult OnPostAddTransaction(int? journal)
        {
            int journalId;
            if(journal == null)
            {
                JournalData newJournal = new JournalData
                {
                    usetID = UserInfo.ID,
                    JournalStatus = JournalData.Status.incomplete
                };
                _context.Attach(newJournal).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _context.SaveChanges();
                journalId = newJournal.ID;
            }
            else
            {
                journalId = journal.Value;
            }

            NewTransaction.Journal = journalId;
            _context.Attach(NewTransaction).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();

            return Redirect($"./Journal?journal={journalId}");
        }

        public IActionResult OnPostRemoveTransaction(int transaction, int? journal)
        {
            TransactionData t = (from m in _context.TransactionData where m.ID == transaction select m).FirstOrDefault();
            _context.Attach(t).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();

            return Redirect($"./Journal?journal={journal.Value}");
        }

        public IActionResult OnPostSubmit(int journal)
        {
            double total = 0;
            foreach(var t in (from t in _context.TransactionData where t.Journal == journal select new { c = t.Credits, d = t.Debits }))
            {
                total += t.c;
                total -= t.d;
            }
            if(total != 0)
            {
                return Redirect($"./JournalErr?journal={journal}");
            }

            if(UserInfo.Class == AccountType.Manager) //Manager's journals are automatically approved
            {
                JournalData j = (from m in _context.JournalData where m.ID == journal select m).FirstOrDefault();
                j.JournalStatus = JournalData.Status.approved;
                _context.Attach(j).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Redirect("./Journals");
            }
            else //Other journals need to be approved by a manager
            {
                JournalData j = (from m in _context.JournalData where m.ID == journal select m).FirstOrDefault();
                j.JournalStatus = JournalData.Status.pending;
                _context.Attach(j).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Redirect("./PendingJournals");
            }
        }

        public IActionResult OnPostDelete(int journal)
        {
            //Remove all transactions
            foreach(TransactionData t in (from t in _context.TransactionData where t.Journal == journal select t))
            {
                _context.Attach(t).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
            //Remove Journal
            JournalData j = (from m in _context.JournalData where m.ID == journal select m).FirstOrDefault();
            _context.Attach(j).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            _context.SaveChanges();

            return Redirect("./Journals");
        }

        public JournalEntry JournalLine(int i)
        {
            return new JournalEntry
            {
                TransactionName = Transactions[i].Name,
                Credit = Transactions[i].Credits,
                Debit = Transactions[i].Debits,
                Desc = Transactions[i].Description,
                AccountName = (from m in _context.AccountData where m.AccountNumber == Transactions[i].AccountNumber select m.AccountName).FirstOrDefault(),
                Date = Transactions[i].TransactionDate
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

        public class JournalEntry
        {
            public string AccountName { get; set; }
            public string TransactionName { get; set; }
            public double Debit { get; set; }
            public double Credit { get; set; }
            public string Desc { get; set; }
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
        }
    }
}