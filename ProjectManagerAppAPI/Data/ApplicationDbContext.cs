using Microsoft.EntityFrameworkCore;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Unit)
                .WithMany()
                .HasForeignKey(s => s.UnitId)
                .IsRequired();

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Currency)
                .WithMany()
                .HasForeignKey(s => s.CurrencyId)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.ContactPersons)
                .WithMany(cp => cp.Customers)
                .UsingEntity(j => j.ToTable("CustomersContactPersons"));

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);
        }
    }
}
