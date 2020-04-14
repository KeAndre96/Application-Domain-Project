using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AppDomainProject
{
    public class View_Trial_BalenceModel : AuthenticatedPageModel
    {
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

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

        [BindProperty]
        public double[] array { get; set; }

        [BindProperty]
        public SortedDictionary<string, double[]> table { get; set; }

        [BindProperty]
        public SortedDictionary<string, double[]> complete_list { get; set; }

        [BindProperty]
        public List<string> keys { get; set; }

        [BindProperty]
        public List<string> names { get; set; }

        public View_Trial_BalenceModel(AppDomainProjectContext context) : base(context)
        {
        }

        public IActionResult OnGet(DateTime? date)
        {
            Date = date.HasValue ? date.Value : DateTime.Now;


            //var query = from m in _context.TransactionData select m;

            // Hold all the transaction data in the dates provided into a list
            var query = from TransactionData a in _context.TransactionData where a.TransactionDate < Date select a;
            AccountTransactions = query.ToList();

            // Hold all the transaction data in the dates provided into a list
            //AccountTransactions = GetTransactions(trueBegin, trueEnd);

            // An array to hold the balence for each of the credits and debits respectfully in an account
            array = new double[2];

            // Hashtable to hold the account with balence
            table = new SortedDictionary<string, double[]>();

            //TODO FIX THE OVERRIDING VALUES IN HASHTABLE

            // Loop through each transaction, updating the array with the sums of credits and debits for each account
            foreach (var transaction in AccountTransactions)
            {
                if (table.ContainsKey(transaction.AccountNumber))
                {
                    if(transaction.Credits != 0)
                    {
                        double[] new_array1 = (double[])table[transaction.AccountNumber];
                        new_array1[0] += transaction.Credits;
                        //table.Remove(transaction.AccountNumber);
                        table[transaction.AccountNumber] = new_array1;
                        //table.Add(transaction.AccountNumber, array);
                    }
                    else
                    {
                        double[] new_array2 = (double[])table[transaction.AccountNumber];
                        new_array2[1] += transaction.Debits;
                        //table.Remove(transaction.AccountNumber);
                        table[transaction.AccountNumber] = new_array2;
                        //table.Add(transaction.AccountNumber, array);
                    }
                }
                else
                {
                    double[] new_array3 = { transaction.Credits, transaction.Debits };
                    table.Add(transaction.AccountNumber, new_array3);
                }
                
            }

            // Loop through the table to subtract credits from debits
            double[] temp = new double[2];
            total_credit_balence = 0;
            total_debit_balence = 0;

            // Hold the complete list of transactions
            complete_list = new SortedDictionary<string, double[]>();

            foreach (var item in table)
            {
                temp = (double[]) item.Value;

                // Credits are larger than debits
                if (temp[1] < temp[0])
                {
                    temp[0] = temp[0] - temp[1];
                    // update the total balence for the credits column to be displayed
                    total_credit_balence += temp[0];
                    temp[1] = 0;
                    complete_list.Add((string)item.Key, temp);
                }
                // Debits are larger than credits
                else
                {
                    temp[1] = temp[1] - temp[0];
                    // update the total balence for the debits column to be displayed
                    total_debit_balence += temp[1];
                    temp[0] = 0;
                    complete_list.Add((string)item.Key, temp);
                }
            }

            
            keys = complete_list.Keys.Cast<string>().ToList();
            
            var q = from m in _context.AccountData select m;
            Accounts = q.ToList();
            names = new List<string>();
            foreach (AccountData data in Accounts)
            {
                if (keys.Contains(data.AccountNumber))
                {
                    names.Add(data.AccountName);
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            return Redirect($"./View_Trial_Balence?date={Date}");
        }
    }
}