using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Implementations.CalculateStrategies;

public class CalculateMinStrategy : ICalculateStrategy
{
    public decimal Calculate(IEnumerable<Employee> employees)
    {
        return employees.Min(x => x.Salary);
    }
}