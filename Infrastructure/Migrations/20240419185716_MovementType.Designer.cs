﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(BootcampContext))]
    [Migration("20240419185716_MovementType")]
    partial class MovementType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Balance")
                        .HasPrecision(20, 5)
                        .HasColumnType("numeric(20,5)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("Holder")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("Account_pkey");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Core.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id")
                        .HasName("Bank_pkey");

                    b.ToTable("Bank", (string)null);
                });

            modelBuilder.Entity("Core.Entities.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AvaibleCredit")
                        .HasColumnType("numeric(20,5)");

                    b.Property<int>("CVV")
                        .HasColumnType("integer");

                    b.Property<int>("CardNumber")
                        .HasColumnType("integer");

                    b.Property<int>("CreditCardStatus")
                        .HasColumnType("integer");

                    b.Property<decimal>("CreditLimit")
                        .HasColumnType("numeric(20,5)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<decimal>("CurrentDebt")
                        .HasColumnType("numeric(20,5)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("Designation")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("date");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("numeric(20,5)");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("date");

                    b.HasKey("Id")
                        .HasName("CreditCard_pkey");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CreditCards", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BuyValue")
                        .HasColumnType("numeric(20,5)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("SellValue")
                        .HasColumnType("numeric(20,5)");

                    b.HasKey("Id")
                        .HasName("Currency_pkey");

                    b.ToTable("Currency", (string)null);
                });

            modelBuilder.Entity("Core.Entities.CurrentAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Interest")
                        .HasColumnType("numeric(10,5)");

                    b.Property<decimal?>("MonthAverage")
                        .HasColumnType("numeric(20,5)");

                    b.Property<decimal?>("OperationalLimit")
                        .HasColumnType("numeric(20,5)");

                    b.HasKey("Id")
                        .HasName("CurrentAccount_pkey");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("CurrentAccounts");
                });

            modelBuilder.Entity("Core.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasPrecision(20, 5)
                        .HasColumnType("text");

                    b.Property<int>("BankId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Birth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreditCardId")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerStatus")
                        .HasColumnType("integer");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Lastname")
                        .HasPrecision(20, 5)
                        .HasColumnType("text");

                    b.Property<string>("Mail")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .HasPrecision(20, 5)
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("Customers_pkey");

                    b.HasIndex("BankId");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Enterprise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Enterprises");
                });

            modelBuilder.Entity("Core.Entities.Movement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Amount")
                        .HasPrecision(20, 5)
                        .HasColumnType("numeric(20,5)");

                    b.Property<string>("Destination")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<int>("MovementType")
                        .HasColumnType("integer");

                    b.Property<int>("TransferStatus")
                        .HasMaxLength(50)
                        .HasColumnType("integer");

                    b.Property<DateTime?>("TransferredDateTime")
                        .HasColumnType("Date");

                    b.HasKey("Id")
                        .HasName("Movement_pkey");

                    b.HasIndex("AccountId");

                    b.ToTable("Movements");
                });

            modelBuilder.Entity("Core.Entities.ProductRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ApprovalDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ProductName")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("ProductRequests");
                });

            modelBuilder.Entity("Core.Entities.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Discount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("Core.Entities.PromotionEnterprise", b =>
                {
                    b.Property<int>("PromotionId")
                        .HasColumnType("integer");

                    b.Property<int>("EnterpriseId")
                        .HasColumnType("integer");

                    b.HasKey("PromotionId", "EnterpriseId");

                    b.HasIndex("EnterpriseId");

                    b.ToTable("PromotionEnterprises");
                });

            modelBuilder.Entity("Core.Entities.SavingAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<string>("HolderName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("SavingType")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("SavingAccount_pkey");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("SavingAccounts");
                });

            modelBuilder.Entity("Core.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int?>("DestinationAccountId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("DestinationAccountId");

                    b.Property<string>("DestinationAccountNumber")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("DestinationDocumentNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("OriginAccountId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("OriginAccountId");

                    b.Property<DateTime>("TransactionDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Core.Entities.Account", b =>
                {
                    b.HasOne("Core.Entities.Currency", "Currency")
                        .WithMany("Accounts")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Core.Entities.CreditCard", b =>
                {
                    b.HasOne("Core.Entities.Currency", "Currency")
                        .WithMany("CreditCards")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Customer", "Customer")
                        .WithMany("CreditCards")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Core.Entities.CurrentAccount", b =>
                {
                    b.HasOne("Core.Entities.Account", "Account")
                        .WithOne("CurrentAccount")
                        .HasForeignKey("Core.Entities.CurrentAccount", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Core.Entities.Customer", b =>
                {
                    b.HasOne("Core.Entities.Bank", "Bank")
                        .WithMany("Customers")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Core.Entities.Movement", b =>
                {
                    b.HasOne("Core.Entities.Account", "Account")
                        .WithMany("Movements")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Core.Entities.ProductRequest", b =>
                {
                    b.HasOne("Core.Entities.Currency", "Currency")
                        .WithMany("Products")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Customer", "Customer")
                        .WithMany("Products")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Core.Entities.PromotionEnterprise", b =>
                {
                    b.HasOne("Core.Entities.Enterprise", "Enterprise")
                        .WithMany("PromotionsEnterprises")
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Promotion", "Promotion")
                        .WithMany("PromotionsEnterprises")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enterprise");

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("Core.Entities.SavingAccount", b =>
                {
                    b.HasOne("Core.Entities.Account", "Account")
                        .WithOne("SavingAccount")
                        .HasForeignKey("Core.Entities.SavingAccount", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Core.Entities.Transaction", b =>
                {
                    b.HasOne("Core.Entities.Currency", "Currency")
                        .WithMany("Transactions")
                        .HasForeignKey("CurrencyId");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("Core.Entities.Account", b =>
                {
                    b.Navigation("CurrentAccount");

                    b.Navigation("Movements");

                    b.Navigation("SavingAccount");
                });

            modelBuilder.Entity("Core.Entities.Bank", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Core.Entities.Currency", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("CreditCards");

                    b.Navigation("Products");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Core.Entities.Customer", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("CreditCards");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Core.Entities.Enterprise", b =>
                {
                    b.Navigation("PromotionsEnterprises");
                });

            modelBuilder.Entity("Core.Entities.Promotion", b =>
                {
                    b.Navigation("PromotionsEnterprises");
                });
#pragma warning restore 612, 618
        }
    }
}
