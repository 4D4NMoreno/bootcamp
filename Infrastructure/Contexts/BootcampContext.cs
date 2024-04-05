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

    public virtual DbSet<Customers> Customers { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<CurrentAccount> CurrentAccounts { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());

        modelBuilder.ApplyConfiguration(new BankConfiguration());

        modelBuilder.ApplyConfiguration(new SavingAccountConfiguration());

        modelBuilder.ApplyConfiguration(new CustomersConfiguration());

        modelBuilder.ApplyConfiguration(new CurrentAccountConfiguration());

        modelBuilder.ApplyConfiguration(new MovementConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
