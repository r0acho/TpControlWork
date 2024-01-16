using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Implementations.CalculateStrategies;

public class CalculateAverageStrategy : ICalculateStrategy
{
    public decimal Calculate(IEnumerable<Employee> employees)
    {
        return employees.Average(x => x.Salary);
    }
}
