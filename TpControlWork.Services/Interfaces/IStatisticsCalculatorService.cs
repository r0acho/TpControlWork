using TpControlWork.Services.Implementations.CalculateStrategies;

namespace TpControlWork.Services.Interfaces;

public interface IStatisticsCalculatorService
{
    ICalculateStrategy? Strategy { get; set; }

    Task<decimal> CalculateByStrategyAsync(IEnumerable<int>? employeeIds);
}
