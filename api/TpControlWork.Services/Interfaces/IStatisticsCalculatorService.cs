using TpControlWork.Domain.Models;
using TpControlWork.Services.Implementations.CalculateStrategies;

namespace TpControlWork.Services.Interfaces;

public interface IStatisticsCalculatorService
{
    ICalculateStrategy? CalculateStrategy { get; set; }

    Task<decimal> CalculateByStrategyAsync(IEnumerable<int>? employeeIds);

    decimal CalculateByStrategy(IEnumerable<Employee> employees);
}
