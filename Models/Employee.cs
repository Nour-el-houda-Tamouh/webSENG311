using System.ComponentModel.DataAnnotations;

namespace LAB6.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        [MinLength(3)]
        public string Position { get; set; }
        public string Image { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public SalaryInfo SalaryInfo { get; set; }
    }

}
