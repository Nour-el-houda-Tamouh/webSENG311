using LAB6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LAB6.Data
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                            .IsRequired()
                            .HasMaxLength(100);
            builder.Property(c => c.Zipcode)
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsFixedLength();
            builder.Property(c => c.City)
                    .HasMaxLength(50);
            builder.Property(c => c.Country)
                        .HasMaxLength(60);
            builder.HasData(
                new Company { Id = 1, Name = "Acme Corporation", Zipcode = 10001, City = "New York", Country = "USA" },
                new Company { Id = 2, Name = "Tech Solutions Ltd.", Zipcode = 94105, City = "San Francisco", Country = "USA" },
                new Company { Id = 3, Name = "EuroTech Innovators", Zipcode = 75001, City = "Paris", Country = "France" },
                new Company { Id = 4, Name = "Sunrise Global", Zipcode = 04854, City = "Singapore", Country = "Singapore" },
                new Company { Id = 5, Name = "Down Under Enterprises", Zipcode = 3000, City = "Melbourne", Country = "Australia" }
            );
            builder.HasMany(c => c.Employees)
           .WithOne(e => e.Company)
           .HasForeignKey(e => e.CompanyId);

        }
    }
}
