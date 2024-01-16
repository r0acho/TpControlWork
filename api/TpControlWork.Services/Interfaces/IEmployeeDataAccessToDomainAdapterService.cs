using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Interfaces;

public interface IEmployeeDataAccessToDomainAdapterService
{
    Employee ConvertToDomainEmployee(DataAccess.Entities.Employee employeeFromDataAccess);
}
