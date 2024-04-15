﻿
using Core.Data.Configurations;
using Core.Entities;
using Core.EntityConfigurations;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class BootcampContext : DbContext
{
    public BootcampContext()
    {
    }

    public BootcampContext(DbContextOptions<BootcampContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<SavingAccount> SavingAccounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<CurrentAccount> CurrentAccounts { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }    

    public virtual DbSet<Enterprise> Enterprises { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());

        modelBuilder.ApplyConfiguration(new BankConfiguration());

        modelBuilder.ApplyConfiguration(new SavingAccountConfiguration());

        modelBuilder.ApplyConfiguration(new CustomersConfiguration());

        modelBuilder.ApplyConfiguration(new CurrentAccountConfiguration());

        modelBuilder.ApplyConfiguration(new MovementConfiguration());

        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());

        modelBuilder.ApplyConfiguration(new CreditCardConfiguration());

        modelBuilder.ApplyConfiguration(new PromotionConfiguration());

        modelBuilder.ApplyConfiguration(new EnterpriseConfiguration());












        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
