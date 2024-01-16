using TpControlWork.Services.Interfaces;
using TpControlWork.Services.Implementations.CalculateStrategies;
using TpControlWork.Domain.Models;

namespace TpControlWork.Services.Implementations;

public class StatisticsCalculatorService : IStatisticsCalculatorService
{
    private readonly IEmployeeService _employeeService;

    public StatisticsCalculatorService(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public ICalculateStrategy? Strategy { get; set; }

    public async Task<decimal> CalculateByStrategyAsync(IEnumerable<int>? employeeIds)
    {
        if (Strategy is null) throw new InvalidOperationException("Calculate strategy is not set");

        var employees = employeeIds is not null
            ? (await _employeeService.GetByIdsAsync(employeeIds.ToArray()))
            : (await _employeeService.GetAllAsync());

        return Strategy.Calculate(employees);
    }

    public decimal CalculateByStrategy(IEnumerable<Employee> employees) => Strategy is not null
        ? Strategy.Calculate(employees)
        : throw new InvalidOperationException("Calculate strategy is not set");
}
