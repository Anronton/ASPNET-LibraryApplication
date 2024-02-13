using Lib.Entities;
using Microsoft.EntityFrameworkCore;


namespace Lib;

public class LibDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<Loan> Loans { get; set; }

    public LibDbContext(DbContextOptions<LibDbContext> options) : base(options)
    {
        // vi justerar Connectionstring inuti Program.cs istället!
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .Property(b => b.IsAvailable)
            .HasDefaultValue(true);
    }

}
