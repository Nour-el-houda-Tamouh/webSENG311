using LAB6.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB6.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<SalaryInfo> SalaryInfo { get; set; }

        public EmployeeContext() { }
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EmployeeDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.Entity<SalaryInfo>(entity =>
            {
                entity.HasKey(si => si.EmployeeId);
                entity.Property(si => si.Net).IsRequired()
                        .HasColumnType("decimal(18,2)");
                entity.Property(si => si.Gross).IsRequired()
                        .HasColumnType("decimal(18,2)");
                entity.HasIndex(e => e.EmployeeId, "IX_SalaryInfo_EmployeeId")
                        .IsUnique();
            });
        }
    }
}

