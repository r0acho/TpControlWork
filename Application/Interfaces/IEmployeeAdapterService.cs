using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Interfaces;

public interface IEmployeeAdapterService
{
    Employee ConvertToDomainEmployee(DataAccess.Entities.Employee employeeFromDataAccess);

    DataAccess.Entities.Employee ConvertToDataAccessEmployee(Employee employeeFromDomain);
}
