using TpControlWork.DataAccess.Interfaces;
using TpControlWork.Domain.Models;
using TpControlWork.Services.Interfaces;

namespace TpControlWork.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeAdapterService _employeeAdapterService;

    public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeAdapterService employeeAdapterService)
    {
        _employeeRepository = employeeRepository;
        _employeeAdapterService = employeeAdapterService;
    }

    public async Task<Employee> CreateAsync(Employee newEntity)
    {
        var dataAccessEmployee = _employeeAdapterService.ConvertToDataAccessEmployee(newEntity);
        await _employeeRepository.CreateAsync(dataAccessEmployee);
        return _employeeAdapterService.ConvertToDomainEmployee(dataAccessEmployee);
    }

    public async Task DeleteAsync(int id)
    {
        var employee = (await _employeeRepository.GetByIdsAsync(id)).First();
        await _employeeRepository.DeleteAsync(employee);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        var employeesFromDataAccess = await _employeeRepository.GetAllAsync();
        return employeesFromDataAccess.Select(_employeeAdapterService.ConvertToDomainEmployee).ToList();
    }

    public async Task<IEnumerable<Employee>> GetByIdsAsync(params int[] ids)
    {
        var employeesFromDataAccess = await _employeeRepository.GetByIdsAsync(ids);
        return employeesFromDataAccess.Select(_employeeAdapterService.ConvertToDomainEmployee).ToList();
    }

    public async Task UpdateAsync(Employee updatedEntity)
    {
        var dataAccessEmployee = _employeeAdapterService.ConvertToDataAccessEmployee(updatedEntity);
        await _employeeRepository.UpdateAsync(dataAccessEmployee);
    }
}
