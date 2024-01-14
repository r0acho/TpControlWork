using Microsoft.EntityFrameworkCore;
using TpControlWork.DataAccess.Entities;

namespace TpControlWork.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<EmployeeType> EmployeeTypes { get; set; }

    public DbSet<PaymentType> PaymentTypes { get; set; }

    public DbSet<Earning> Earnings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.FkEmployeeType)
            .WithMany()
            .HasForeignKey(e => e.FkEmployeeTypeId);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.FkPaymentType)
            .WithMany()
            .HasForeignKey(e => e.FkPaymentTypeId);

        modelBuilder.Entity<Earning>()
            .HasOne(e => e.FkEmployee)
            .WithMany(e => e.Earnings)
            .HasForeignKey(e => e.FkEmployeeId);

        modelBuilder.Entity<Earning>()
            .HasOne(e => e.FkEmployee)
            .WithMany()
            .HasForeignKey(e => e.FkEmployeeId);

        base.OnModelCreating(modelBuilder);
    }
}