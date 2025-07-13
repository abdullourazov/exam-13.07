using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext<IdentityUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>()
            .HasMany(c => c.Rentals)
            .WithOne(r => r.Car)
            .HasForeignKey(r => r.CarId);

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Rentals)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId);

        modelBuilder.Entity<Branch>()
            .HasMany(b => b.Cars)
            .WithOne(c => c.Branches) 
            .HasForeignKey(c => c.BranchId);
    }
}
