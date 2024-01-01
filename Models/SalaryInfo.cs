using System.ComponentModel.DataAnnotations;

namespace LAB6.Models
{
    public class SalaryInfo
    {
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Required]
        public decimal Net { get; set; }
        [Required]
        public decimal Gross { get; set; }
    }
}
