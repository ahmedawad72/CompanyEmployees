using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    // COMPANY
    public abstract record CompanyForManipulationDto
    {
        [Required(ErrorMessage = "Company Name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for the name is 60 characters.")]
        public string Name { get; init; } = default!;
        [Required(ErrorMessage = "Company Address is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum length for company address is 60 characters.")]
        public string Address { get; init; } = default!;
        public string Country { get; init; } = default!;
        public IEnumerable<EmployeeForCreationDto> Employees { get; init; } = Array.Empty<EmployeeForCreationDto>();
    }
    public record CompanyForCreationDto : CompanyForManipulationDto;
    public record CompanyForUpdateDto : CompanyForManipulationDto;


    // EMPLOYEE
    public abstract record EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }

        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
        public int Age { get; init; }
        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string? Position { get; init; }
    }
    public record EmployeeForCreationDto:EmployeeForManipulationDto;
    public record EmployeeForUpdateDto:EmployeeForManipulationDto;
}
    