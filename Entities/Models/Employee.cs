
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Employee
    {
        // The [Column] attribute will specify that the Id property is going to be mapped with a different name in the database.
        [Column ("EmployeeId")]
        public Guid Id { get; set; }

        [Required (ErrorMessage ="Employee name is a required field ")]
        [MaxLength (30,ErrorMessage ="Max length of Employee name is 30 character")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Employee age is a required field ")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Employee name is a required field ")]
        [MaxLength(20, ErrorMessage = "Max length of Employee name is 20 character")]
        public string? Position { get; set; }
       
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        public Company? Company { get; set; }
    }
}
