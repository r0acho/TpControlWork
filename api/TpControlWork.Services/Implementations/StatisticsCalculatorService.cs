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

    public ICalculateStrategy? CalculateStrategy { get; set; }

    public async Task<decimal> CalculateByStrategyAsync(IEnumerable<int>? employeeIds)
    {
        if (CalculateStrategy is null) throw new InvalidOperationException("Calculate strategy is not set");

        var employees = employeeIds is not null
            ? (await _employeeService.GetByIdsAsync(employeeIds.ToArray()))
            : (await _employeeService.GetAllAsync());

        return CalculateStrategy.Calculate(employees);
    }

    public decimal CalculateByStrategy(IEnumerable<Employee> employees) => CalculateStrategy is not null
        ? CalculateStrategy.Calculate(employees)
        : throw new InvalidOperationException("Calculate strategy is not set");
}
