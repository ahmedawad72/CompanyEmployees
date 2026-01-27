using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>()
               .ForMember(c => c.FullAddress,
               opt => opt.MapFrom(src => string.Join(' ', src.Address, src.Country)));

            CreateMap<Employee, EmployeeDto>();
        }

    }
}
