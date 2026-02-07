using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CompanyForCreationDto
    {
        public string Name { get; init; } = default!;
        public string Address { get; init; } = default!;
        public string Country { get; init; } = default!;
        public IEnumerable<EmployeeForCreationDto> Employees { get; init; } = Array.Empty<EmployeeForCreationDto>();
    }

    public record EmployeeForCreationDto
    {
        public string Name { get; init; } = default!;
        public int Age { get; init; }     
        public string Position { get; init; } = default!;
    }
    public record EmployeeForUpdateDto
    {
        public string Name { get; init; } = default!;
        public int Age { get; init; }
        public string Position { get; init; } = default!;
    }
    public record class CompanyForUpdateDto
    {
        public string Name { get; init; } = default!;
        public string Address { get; init; } = default!;
        public string Country { get; init; } = default!;
        public IEnumerable<EmployeeForUpdateDto> Employees { get; init; } = Array.Empty<EmployeeForUpdateDto>();
    }
}
    