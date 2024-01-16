using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Implementations.CalculateStrategies;

public class CalculateMaxStrategy : ICalculateStrategy
{
    public decimal Calculate(IEnumerable<Employee> employees)
    {
        return employees.Max(x => x.Salary);
    }
}
