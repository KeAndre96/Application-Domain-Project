﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject.Models
{
    public class AppDomainProjectContext : DbContext
    {
        public AppDomainProjectContext (DbContextOptions<AppDomainProjectContext> options)
            : base(options)
        {
        }


        public DbSet<PasswordData> LoginData { get; set; }

        public DbSet<UserInfoData> UserInfoData { get; set; }

        public DbSet<PersonalInfoData> PersonalInfoData { get; set; }

        public DbSet<AccountData> AccountData { get; set; }

        public DbSet<TransactionData> TransactionData { get; set; }

        public DbSet<JournalData> JournalData { get; set; }

        public DbSet<EventLogData> EventLogData { get; set; }
    }
}
