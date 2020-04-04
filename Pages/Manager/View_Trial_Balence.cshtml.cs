using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;

namespace AppDomainProject
{
    public class View_Trial_BalenceModel : AuthenticatedPageModel
    {
        [BindProperty]
        public List<TransactionData> AccountTransactions { get; set; }

        [BindProperty]
        public AccountData Account { get; set; }

        [BindProperty]
        public List<AccountData> Accounts { get; set; }

        [BindProperty]
        public double total_credit_balence { get; set; }
        [BindProperty]
        public double total_debit_balence { get; set; }

        public View_Trial_BalenceModel(AppDomainProjectContext context) : base(context)
        {
        }

        public IActionResult OnGet(DateTime? begin, DateTime? end)
        {
            DateTime trueBegin = begin.HasValue ? begin.Value : DateTime.MinValue;
            DateTime trueEnd = end.HasValue ? end.Value : DateTime.MaxValue;
            var query = from m in _context.TransactionData select m;

            // Hold all the transaction data in the dates provided into a list
            AccountTransactions = query.ToList();

            // Hold all the transaction data in the dates provided into a list
            //AccountTransactions = GetTransactions(trueBegin, trueEnd);

            // An array to hold the balence for each of the credits and debits respectfully in an account
            double[] array = new double[2];

            // Hashtable to hold the account with balence
            Hashtable table = new Hashtable();

            //TODO FIX THE OVERRIDING VALUES IN HASHTABLE

            // Loop through each transaction, updating the array with the sums of credits and debits for each account
            foreach (var transaction in AccountTransactions)
            {
                if (table.ContainsKey(transaction.AccountNumber))
                {
                    if(transaction.Credits != 0)
                    {
                        array = (double[])table[transaction.AccountNumber];
                        array[0] += transaction.Credits;
                        table.Remove(transaction.AccountNumber);
                        table.Add(transaction.AccountNumber, array);
                    }
                    else
                    {
                        array = (double[])table[transaction.AccountNumber];
                        array[1] += transaction.Debits;
                        table.Remove(transaction.AccountNumber);
                        table.Add(transaction.AccountNumber, array);
                    }
                }
                else
                {
                    array[0] = transaction.Credits;
                    array[1] = transaction.Debits;
                    table.Add(transaction.AccountNumber, array);
                }
            }

            // Loop through the table to subtract credits from debits
            double[] temp = new double[2];
            total_credit_balence = 0;
            total_debit_balence = 0;

            // Hold the complete list of transactions
            Hashtable complete_list = new Hashtable();

            foreach (DictionaryEntry item in table)
            {
                temp = (double[]) item.Value;

                // Credits are larger than debits
                if (temp[0] < temp[1])
                {
                    temp[0] = temp[0] - temp[1];
                    // update the total balence for the credits column to be displayed
                    total_credit_balence += temp[0];
                    temp[1] = 0;
                    complete_list.Add(item.Key, temp);
                }
                // Debits are larger than credits
                else
                {
                    temp[1] = temp[1] - temp[0];
                    // update the total balence for the debits column to be displayed
                    total_debit_balence += temp[1];
                    temp[0] = 0;
                    complete_list.Add(item.Key, temp);
                }
            }
            return Page();

        }
        
        /*
        private List<TransactionData> GetTransactions(DateTime start, DateTime end)
        {
            var q = from t in _context.TransactionData join j in _context.JournalData on t.Journal equals j.ID where j.JournalStatus == JournalData.Status.approved;
            q = q.Where(t => t.TransactionDate >= start);
            q = q.Where(t => t.TransactionDate <= end);
            List<TransactionData> list = q.ToList();
            list.Sort((a, b) => { return a.TransactionDate.CompareTo(b.TransactionDate); });
            return list;
        }
        */
    }
}