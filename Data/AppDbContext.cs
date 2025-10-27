using Microsoft.EntityFrameworkCore;
using FinanTech.Models;

namespace FinanTech.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    public DbSet<LoanRequest> LoanRequests { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.AccountId);
            entity.Property(a => a.Balance).HasColumnType("REAL");
            entity.Property(a => a.Owner).IsRequired();
            entity.Property(a => a.RowVersion).IsRowVersion();
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Amount).HasColumnType("REAL");
            entity.Property(t => t.Timestamp).IsRequired();
        });

        modelBuilder.Entity<LoanRequest>(entity =>
        {
            entity.HasKey(l => l.Id);
            entity.Property(l => l.ApplicantName).IsRequired();
            entity.Property(l => l.Amount).HasColumnType("REAL");
            entity.Property(l => l.Reason).IsRequired();
            entity.Property(l => l.Status).IsRequired();
            entity.Property(l => l.CreatedAt).IsRequired();
        });
    }
}
