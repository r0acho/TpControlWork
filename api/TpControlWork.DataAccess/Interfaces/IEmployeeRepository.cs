using TpControlWork.DataAccess.Entities;

namespace TpControlWork.DataAccess.Interfaces;

public interface IEmployeeRepository
{
    Task CreateAsync(Employee newEntity);

    Task<IEnumerable<Employee>> GetByIdsAsync(params int[] ids);

    Task<IEnumerable<Employee>> GetAllAsync();

    Task UpdateAsync(Employee updatedEntity);

    Task DeleteAsync(Employee deletingEntity);
}
