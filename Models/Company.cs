using System.ComponentModel.DataAnnotations;

namespace LAB6.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(5)]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Zipcode must be 5 digits.")]
        public int Zipcode { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(60)]
        public string Country { get; set; }


        public List<Employee> Employees { get; set; }
    }
}
