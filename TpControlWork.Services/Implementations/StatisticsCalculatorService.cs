using TpControlWork.Services.Interfaces;
using TpControlWork.Services.Implementations.CalculateStrategies;

namespace TpControlWork.Services.Implementations;

public class StatisticsCalculatorService : IStatisticsCalculatorService
{
    private readonly IEmployeeService _employeeService;

    public StatisticsCalculatorService(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public ICalculateStrategy? Strategy { get; set; }

    public async Task<decimal> CalculateAverageAsync(IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds is not null) return (await _employeeService.GetByIdsAsync(
            employeeIds.ToArray())).Average(x => x.Salary);

        return (await _employeeService.GetAllAsync()).Average(x => x.Salary);
    }

    public async Task<decimal> CalculateByStrategyAsync(IEnumerable<int>? employeeIds)
    {
        if (Strategy is null) throw new InvalidOperationException("Calculate strategy is not set");

        var employees = employeeIds is not null
            ? (await _employeeService.GetByIdsAsync(employeeIds.ToArray()))
            : (await _employeeService.GetAllAsync());

        return Strategy.Calculate(employees);
    }
}
