using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class DBSetupModel : PageModel
    {

        private AppDomainProjectContext _context;

        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public List<string> Defaults { get; set; }

        public DBSetupModel(AppDomainProjectContext context)
        {
            _context = context;
        }

        public void OnGet(bool? force)
        {
            Defaults = new List<string>();
            var q = from m in _context.AccountData select m;
            if(q.FirstOrDefault() == null || (force != null && force.Value)) //Database is not set up, it needs to be done
            {
                Setup();
                Message = "Data initialized!";
            }
            else
            {
                Message = "Setup already complete";
            }
        }

        private void Add(object dbo, object key)
        {
            if(_context.Find(dbo.GetType(), key) == null)
            {
                _context.Attach(dbo).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }
        }

        private void Setup()
        {
            //Database setup goes here
            //Admin Account
            UserInfoData adminInfo = new UserInfoData
            {
                Status = AccountStatus.Active,
                ID = "bs0120",
                Class = AccountType.Admin,
                Email = "bob.smith@admin.photon.investments",
                PasswordSetDate = new DateTime(2020, 1, 1),
                PasswordExpirationDate = new DateTime(2020, 4, 30)
            };
            Add(adminInfo, adminInfo.ID);
            PersonalInfoData adminPersonal = new PersonalInfoData
            {
                FirstName = "Bob",
                LastName = "Smith",
                ID = "bs0120",
                Address = "123 Main Street, Marietta, GA",
                DOB = new DateTime(1980, 07, 12)
            };
            Add(adminPersonal, adminPersonal.ID);
            PasswordData adminPass = new PasswordData
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
            };
            Add(adminPass, adminPass.ID);
            Defaults.Add("<b>Admin</b><br>");
            Defaults.Add("ID: bs0120<br>");
            Defaults.Add("Password: Password<br>");

            //Manager Account
            UserInfoData managerInfo = new UserInfoData
            {
                Status = AccountStatus.Active,
                ID = "sg0120",
                Class = AccountType.Manager,
                Email = "saul.goodman@manager.photon.investments",
                PasswordSetDate = new DateTime(2020, 1, 1),
                PasswordExpirationDate = new DateTime(2020, 4, 30)
            };
            Add(managerInfo, managerInfo.ID);
            PersonalInfoData managerPersonal = new PersonalInfoData
            {
                FirstName = "Saul",
                LastName = "Goodman",
                ID = "sg0120",
                Address = "123 Main Street, Marietta, GA",
                DOB = new DateTime(1987, 01, 03)
            };
            Add(managerPersonal, managerPersonal.ID);
            PasswordData managerPass = new PasswordData
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
            };
            Add(managerPass, managerPass.ID);
            Defaults.Add("<b>Manager</b><br>");
            Defaults.Add("ID: sg0120<br>");
            Defaults.Add("Password: Password<br>");

            //User Account
            UserInfoData userInfo = new UserInfoData
            {
                Status = AccountStatus.Active,
                ID = "jj0120",
                Class = AccountType.Manager,
                Email = "john.jenkins@user.photon.investments",
                PasswordSetDate = new DateTime(2020, 1, 1),
                PasswordExpirationDate = new DateTime(2020, 4, 30)
            };
            Add(userInfo, userInfo.ID);
            PersonalInfoData userPersonal = new PersonalInfoData
            {
                FirstName = "John",
                LastName = "Jenkins",
                ID = "jj0120",
                Address = "123 Main Street, Marietta, GA",
                DOB = new DateTime(1990, 12, 22)
            };
            Add(userPersonal, userPersonal.ID);
            PasswordData userPass = new PasswordData
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
            };
            Add(userPass, userPass.ID);
            Defaults.Add("<b>Accountant</b><br>");
            Defaults.Add("ID: jj0120<br>");
            Defaults.Add("Password: Password<br>");

            _context.SaveChanges();
        }
    }
}