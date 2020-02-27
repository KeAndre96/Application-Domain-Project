﻿// <auto-generated />
using System;
using AppDomainProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppDomainProject.Migrations
{
    [DbContext(typeof(AppDomainProjectContext))]
    [Migration("20200221151500_transactions")]
    partial class transactions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppDomainProject.Models.AccountData", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountSubCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Credit")
                        .HasColumnType("float");

                    b.Property<double>("Debit")
                        .HasColumnType("float");

                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("InitialBalance")
                        .HasColumnType("float");

                    b.Property<bool>("NormalSide")
                        .HasColumnType("bit");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Statement")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeAccountAdded")
                        .HasColumnType("datetime2");

                    b.HasKey("AccountNumber");

                    b.ToTable("AccountData");
                });

            modelBuilder.Entity("AppDomainProject.Models.PasswordData", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityAnswer1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityAnswer2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityAnswer3")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("LoginData");
                });

            modelBuilder.Entity("AppDomainProject.Models.PersonalInfoData", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PersonalInfoData");
                });

            modelBuilder.Entity("AppDomainProject.Models.TransactionData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Ammount")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Journal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TransactionData");
                });

            modelBuilder.Entity("AppDomainProject.Models.UserInfoData", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PasswordExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PasswordSetDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("UserInfoData");
                });
#pragma warning restore 612, 618
        }
    }
}