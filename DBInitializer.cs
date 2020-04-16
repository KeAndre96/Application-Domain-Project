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
                Class = AccountType.Manager,
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
        }

        /// <summary>
        /// Use this method to add AccountData objects
        /// </summary>
        private void AddAccountData()
        {

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
