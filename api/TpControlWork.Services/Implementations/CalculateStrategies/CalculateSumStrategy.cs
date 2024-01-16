using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Implementations.CalculateStrategies;

public class CalculateSumStrategy : ICalculateStrategy
{
    public decimal Calculate(IEnumerable<Employee> employees)
    {
        return employees.Sum(x => x.Salary);
    }
}
