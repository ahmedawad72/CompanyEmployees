using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Runtime.InteropServices;
namespace Service
{ 
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;   
        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployeesAsync
            (Guid companyId,EmployeeParameters employeeParameters ,bool trackChanges)
        {
            if(!employeeParameters.ValidAgeRange)
            {
                throw new MaxAgeRangeBadRequestException();
            }
            await CheckIfCompanyExists(companyId, trackChanges);

            var employeesWithMetaData =await  _repository.Employee.GetEmployeesAsync(companyId,employeeParameters ,trackChanges);  
           
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
        
            return (employeesDto,employeesWithMetaData.MetaData);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            if (employee is null)
            {
                throw new EmployeeNotFoundException(id);
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        public async Task <EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto
           employeeForCreation, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }
        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id , bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeeForCompany = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id,trackChanges);

            _repository.Employee.DeleteEmployee(employeeForCompany);
            await _repository.Save();
        }
        public async Task UpdateEmployeeForCompanyAsync
        (Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool empTrackChanges, bool ComTrackChanges)
        {
            await CheckIfCompanyExists(companyId, ComTrackChanges);
            var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId,id, empTrackChanges);
            _mapper.Map(employeeForUpdate, employeeEntity);
            await _repository.Save();
        }
    
        
        // Private Methods
        private async Task CheckIfCompanyExists(Guid id, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(id);
        }
        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId,Guid id, bool trackChanges)
        { 
            var employee = await _repository.Employee.GetEmployeeAsync (companyId, id, trackChanges);
            if (employee is null)
                throw new EmployeeNotFoundException (companyId);
            return employee;
        }

    }

}
