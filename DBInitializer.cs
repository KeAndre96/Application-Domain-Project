using AppDomainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject
{
    public class DBInitializer
    {
        public DBInitializer(AppDomainProjectContext context)
        {
            _context = context;

            passwordData = new List<PasswordData>();
            userInfoData = new List<UserInfoData>();
            personalInfoData = new List<PersonalInfoData>();
            accountData = new List<AccountData>();
            transactionData = new List<TransactionData>();
            journalData = new List<JournalData>();
        }

        private AppDomainProjectContext _context { get; set; }

        private List<PasswordData> passwordData;
        private List<UserInfoData> userInfoData;
        private List<PersonalInfoData> personalInfoData;
        private List<AccountData> accountData;
        private List<TransactionData> transactionData;
        private List<JournalData> journalData;

        public void Init()
        {
            if((from m in _context.LoginData select m).FirstOrDefault() == null)
            {
                AddPasswordData();
                passwordData.ForEach(o => _context.Add(o).State = Microsoft.EntityFrameworkCore.EntityState.Added);
            }
            if ((from m in _context.PersonalInfoData select m).FirstOrDefault() == null)
            {
                AddPersonalInfoData();
                personalInfoData.ForEach(o => _context.Add(o).State = Microsoft.EntityFrameworkCore.EntityState.Added);
            }
            if ((from m in _context.UserInfoData select m).FirstOrDefault() == null)
            {
                AddUserInfoData();
                userInfoData.ForEach(o => _context.Add(o).State = Microsoft.EntityFrameworkCore.EntityState.Added);
            }
            if ((from m in _context.AccountData select m).FirstOrDefault() == null)
            {
                AddAccountData();
                accountData.ForEach(o => _context.Add(o).State = Microsoft.EntityFrameworkCore.EntityState.Added);
            }
            if ((from m in _context.TransactionData select m).FirstOrDefault() == null)
            {
                AddTransactionData();
                transactionData.ForEach(o => _context.Add(o).State = Microsoft.EntityFrameworkCore.EntityState.Added);
            }
            if ((from m in _context.JournalData select m).FirstOrDefault() == null)
            {
                AddJournalData();
                journalData.ForEach(o => _context.Add(o).State = Microsoft.EntityFrameworkCore.EntityState.Added);
            }

            _context.SaveChanges();
        }


        /*
         ---Population Methods---
         Create objects in these methods and add them to the collections in this class. They will automatically be added to the database if the table is empty.
             */

        /// <summary>
        /// Use this method to add LoginData objects
        /// </summary>
        private void AddPasswordData()
        {
            passwordData.Add(new PasswordData
            {
                ID = "bs0120",
                Password = "password",
                Attempts = 0,
                SecurityQuestion1 = "Question 1",
                SecurityQuestion2 = "Question 2",
                SecurityQuestion3 = "Question 3",
                SecurityAnswer1 = "Answer",
                SecurityAnswer2 = "Answer",
                SecurityAnswer3 = "Answer"
            });

            passwordData.Add(new PasswordData
            {
                ID = "sg0120",
                Password = "password",
                Attempts = 0,
                SecurityQuestion1 = "Question 1",
                SecurityQuestion2 = "Question 2",
                SecurityQuestion3 = "Question 3",
                SecurityAnswer1 = "Answer",
                SecurityAnswer2 = "Answer",
                SecurityAnswer3 = "Answer"
            });

            passwordData.Add(new PasswordData
            {
                ID = "jj0120",
                Password = "password",
                Attempts = 0,
                SecurityQuestion1 = "Question 1",
                SecurityQuestion2 = "Question 2",
                SecurityQuestion3 = "Question 3",
                SecurityAnswer1 = "Answer",
                SecurityAnswer2 = "Answer",
                SecurityAnswer3 = "Answer"
            });

            passwordData.Add(new PasswordData
            {
                ID = "ww0120",
                Password = "password",
                Attempts = 0,
                SecurityQuestion1 = "Question 1",
                SecurityQuestion2 = "Question 2",
                SecurityQuestion3 = "Question 3",
                SecurityAnswer1 = "Answer",
                SecurityAnswer2 = "Answer",
                SecurityAnswer3 = "Answer"
            });
        }

        /// <summary>
        /// Use this method to add UserInfoData objects
        /// </summary>
        private void AddUserInfoData()
        {
            userInfoData.Add(new UserInfoData
            {
                Status = AccountStatus.Active,
                ID = "bs0120",
                Class = AccountType.Admin,
                Email = "bob.smith@admin.photon.investments",
                PasswordSetDate = new DateTime(2020, 2, 1),
                PasswordExpirationDate = new DateTime(2020, 5, 30)
            });

            userInfoData.Add(new UserInfoData
            {
                Status = AccountStatus.Active,
                ID = "sg0120",
                Class = AccountType.Manager,
                Email = "saul.goodman@manager.photon.investments",
                PasswordSetDate = new DateTime(2020, 2, 1),
                PasswordExpirationDate = new DateTime(2020, 5, 30)
            });

            userInfoData.Add(new UserInfoData
            {
                Status = AccountStatus.Active,
                ID = "jj0120",
                Class = AccountType.User,
                Email = "john.jenkins@user.photon.investments",
                PasswordSetDate = new DateTime(2020, 2, 1),
                PasswordExpirationDate = new DateTime(2020, 5, 30)
            });

            userInfoData.Add(new UserInfoData
            {
                Status = AccountStatus.Active,
                ID = "ww0120",
                Class = AccountType.User,
                Email = "john.jenkins@user.photon.investments",
                PasswordSetDate = new DateTime(2020, 2, 1),
                PasswordExpirationDate = new DateTime(2020, 5, 30)
            });
        }

        /// <summary>
        /// Use this method to add PersonalInfoData objects
        /// </summary>
        private void AddPersonalInfoData()
        {
            personalInfoData.Add(new PersonalInfoData
            {
                FirstName = "Bob",
                LastName = "Smith",
                ID = "bs0120",
                Address = "123 Main Street, Marietta, GA",
                DOB = new DateTime(1980, 07, 12)
            });

            personalInfoData.Add(new PersonalInfoData
            {
                FirstName = "Saul",
                LastName = "Goodman",
                ID = "sg0120",
                Address = "123 Main Street, Marietta, GA",
                DOB = new DateTime(1987, 01, 03)
            });

            personalInfoData.Add(new PersonalInfoData
            {
                FirstName = "John",
                LastName = "Jenkins",
                ID = "jj0120",
                Address = "123 Main Street, Marietta, GA",
                DOB = new DateTime(1990, 12, 22)
            });

            personalInfoData.Add(new PersonalInfoData
            {
                FirstName = "Walter",
                LastName = "White",
                ID = "ww0120",
                Address = "123 Main Street, Marietta, GA",
                DOB = new DateTime(1997, 12, 22)
            });
        }

        /// <summary>
        /// Use this method to add AccountData objects
        /// </summary>
        private void AddAccountData()
        {
            accountData.Add( new AccountData
            {
                AccountNumber = "100",
                AccountName = "Accounts Payable",
                AccountDescription = "Money owed by ABC Company to creditors",
                NormalSide = true,
                AccountCategory = "Liability",
                AccountSubCategory = "Expense",
                Debit = 0,
                Credit = 0,
                Balance = 875,
                TimeAccountAdded = DateTime.Now,
                Order = 1,
                Statement = AccountData.AccountStatement.RE,
                Comment = "",
                Active = true
            });

            accountData.Add(new AccountData
            {
                AccountNumber = "200",
                AccountName = "Accounts Recievable",
                AccountDescription = "Money owed to ABC company by its debtors",
                NormalSide = false,
                AccountCategory = "Income",
                AccountSubCategory = "Revenue",
                Debit = 0,
                Credit = 0,
                Balance = 2200,
                TimeAccountAdded = DateTime.Now,
                Order = 2,
                Statement = AccountData.AccountStatement.IS,
                Comment = "",
                Active = true
            });

            accountData.Add( new AccountData
            {
                AccountNumber = "300",
                AccountName = "Cash",
                AccountDescription = "Money Revenue",
                NormalSide = false,
                AccountCategory = "Income",
                AccountSubCategory = "Revenue",
                Debit = 0,
                Credit = 0,
                Balance = 12345,
                TimeAccountAdded = DateTime.Now,
                Order = 3,
                Statement = AccountData.AccountStatement.IS,
                Comment = "",
                Active = true
            });

            accountData.Add( new AccountData
            {
                AccountNumber = "400",
                AccountName = "Supplies",
                AccountDescription = "Expenese coming from gathering supplies",
                NormalSide = true,
                AccountCategory = "Liability",
                AccountSubCategory = "Expense",
                Debit = 0,
                Credit = 0,
                Balance = 1234,
                TimeAccountAdded = DateTime.Now,
                Order = 4,
                Statement = AccountData.AccountStatement.BS,
                Comment = "",
                Active = true
            });

            accountData.Add(new AccountData
            {
                AccountNumber = "500",
                AccountName = "Common Stock",
                AccountDescription = "Shares entitling their holder to dividends that vary in amount and may even be missed, depending on the fortunes of the company",
                NormalSide = false,
                AccountCategory = "Asset",
                AccountSubCategory = "Revenue",
                Debit = 0,
                Credit = 0,
                Balance = 300000,
                TimeAccountAdded = DateTime.Now,
                Order = 5,
                Statement = AccountData.AccountStatement.BS,
                Comment = "",
                Active = true
            });

            accountData.Add(new AccountData
            {
                AccountNumber = "600",
                AccountName = "Service Revenue",
                AccountDescription = "Sales reported by a business that relate to services provided to its customers",
                NormalSide = true,
                AccountCategory = "Revenue",
                AccountSubCategory = "Asset",
                Debit = 0,
                Credit = 0,
                Balance = 5300,
                TimeAccountAdded = DateTime.Now,
                Order = 6,
                Statement = AccountData.AccountStatement.BS,
                Comment = "",
                Active = true
            });

            accountData.Add(new AccountData
            {
                AccountNumber = "700",
                AccountName = "Notes Payable",
                AccountDescription = "Written promissory note",
                NormalSide = true,
                AccountCategory = "Asset",
                AccountSubCategory = "Revenue",
                Debit = 0,
                Credit = 0,
                Balance = 17500,
                TimeAccountAdded = DateTime.Now,
                Order = 7,
                Statement = AccountData.AccountStatement.RE,
                Comment = "",
                Active = true
            });

            accountData.Add(new AccountData
            {
                AccountNumber = "800",
                AccountName = "Prepaid Insurance",
                AccountDescription = "The portion of an insurance premium that has been paid in advance",
                NormalSide = true,
                AccountCategory = "Expense",
                AccountSubCategory = "Current",
                Debit = 0,
                Credit = 0,
                Balance = 1800,
                TimeAccountAdded = DateTime.Now,
                Order = 8,
                Statement = AccountData.AccountStatement.BS,
                Comment = "",
                Active = true
            });

            accountData.Add(new AccountData
            {
                AccountNumber = "900",
                AccountName = "Rent Expense",
                AccountDescription = "Lists the cost of occupying rental property",
                NormalSide = true,
                AccountCategory = "Expense",
                AccountSubCategory = "Current",
                Debit = 0,
                Credit = 0,
                Balance = 1600,
                TimeAccountAdded = DateTime.Now,
                Order = 9,
                Statement = AccountData.AccountStatement.BS,
                Comment = "",
                Active = true
            });
        }

        /// <summary>
        /// Use this method to add TransactionData objects
        /// </summary>
        private void AddTransactionData()
        {

        }

        /// <summary>
        /// Use this method to add JournalData objects
        /// </summary>
        private void AddJournalData()
        {

        }

    }
}
