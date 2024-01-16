using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Interfaces;

public interface IEmployeeDomainToDataAccessAdapterService
{
    DataAccess.Entities.Employee ConvertToDataAccessEmployee(Employee employeeFromDomain);
}
