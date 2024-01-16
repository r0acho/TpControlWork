using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Implementations.CalculateStrategies;

public interface ICalculateStrategy
{
    decimal Calculate(IEnumerable<Employee> employees);
}
