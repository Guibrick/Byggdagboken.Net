using Byggdagboken.Models;
using Microsoft.EntityFrameworkCore;

namespace Byggdagboken.Data;

public partial class PeopleDBContext : DbContext
{
    public PeopleDBContext()
    {
    }

    public PeopleDBContext(DbContextOptions<PeopleDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=People;User Id=sa;Password=Byggdagboken2023;TrustServerCertificate=True;");
    }
}