using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Interfaces;

public interface IEmployeeService
{
    Task<Employee> CreateAsync(Employee newEntity);

    Task<IEnumerable<Employee>> GetByIdsAsync(params int[] ids);

    Task<IEnumerable<Employee>> GetAllAsync();

    Task UpdateAsync(Employee updatedEntity);

    Task DeleteAsync(int id);
}
