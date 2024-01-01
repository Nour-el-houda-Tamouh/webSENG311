using LAB6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAB6.Data
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Surname).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Position).IsRequired().HasMaxLength(100);
            builder.HasOne(e => e.SalaryInfo)
                    .WithOne(si => si.Employee)
                    .HasForeignKey<SalaryInfo>(si => si.EmployeeId);
            builder.HasData(
                 new Employee
                 {
                     Id = 1,
                     Name = "Martin",
                     Surname = "Simpson",
                     BirthDate = new DateTime(1992, 12, 3),
                     Position = "Marketing Expert",
                     Image = "/images/Martin.jpg"
                 },
            new Employee
            {
                Id = 2,
                Name = "Jacob",
                Surname = "Hawk",
                BirthDate = new DateTime(1995, 10, 2),
                Position = "Manager",
                Image = "/images/Jacob.jpg"
            },
            new Employee
            {
                Id = 3,
                Name = "Elizabeth",
                Surname = "Geil",
                BirthDate = new DateTime(2000, 1, 7),
                Position = "Software Engineer",
                Image = "/images/Elizabeth.jpg"
            },
            new Employee
            {
                Id = 4,
                Name = "Kate",
                Surname = "Metain",
                BirthDate = new DateTime(1997, 2, 13),
                Position = "Admin",
                Image = "/images/Kate.jpg"
            },
            new Employee
            {
                Id = 5,
                Name = "Michael",
                Surname = "Cook",
                BirthDate = new DateTime(1990, 12, 25),
                Position = "Marketing expert",
                Image = "/images/Michael.jpg"
            },
            new Employee
            {
                Id = 6,
                Name = "John",
                Surname = "Snow",
                BirthDate = new DateTime(2001, 7, 15),
                Position = "Software Engineer",
                Image = "/images/John.jpg"
            },
            new Employee
            {
                Id = 7,
                Name = "Nina",
                Surname = "Soprano",
                BirthDate = new DateTime(1999, 9, 30),
                Position = "Software Engineer",
                Image = "/images/Nina.jpg"
            },
            new Employee
            {
                Id = 8,
                Name = "Tina",
                Surname = "Fins",
                BirthDate = new DateTime(2000, 5, 14),
                Position = "Team Leader",
                Image = "/images/Tina.jpg"
            });
        }
    }
}
