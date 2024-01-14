using TpControlWork.Services.Interfaces;
using TpControlWork.Services.Extensions;

namespace TpControlWork.Services.Implementations;

public class StatisticsCalculatorService : IStatisticsCalculatorService
{
    private readonly IEmployeeService _employeeService;

    // TODO refactor (вынести получение в отдельный метод)
    public StatisticsCalculatorService(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public async Task<decimal> CalculateAverageAsync(IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds is not null) return (await _employeeService.GetByIdsAsync(
            employeeIds.ToArray())).Average(x => x.Salary);

        return (await _employeeService.GetAllAsync()).Average(x => x.Salary);
    }

    public async Task<decimal> CalculateMaxAsync(IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds is not null) return (await _employeeService.GetByIdsAsync(
            employeeIds.ToArray())).Max(x => x.Salary);

        return (await _employeeService.GetAllAsync()).Max(x => x.Salary);
    }

    public async Task<decimal> CalculateMedianAsync(IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds is not null) return (await _employeeService.GetByIdsAsync(
            employeeIds.ToArray())).Select(x => x.Salary).Median();

        return (await _employeeService.GetAllAsync()).Select(x => x.Salary).Median();
    }

    public async Task<decimal> CalculateMinAsync(IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds is not null) return (await _employeeService.GetByIdsAsync(
            employeeIds.ToArray())).Min(x => x.Salary);

        return (await _employeeService.GetAllAsync()).Min(x => x.Salary);
    }

    public async Task<decimal> CalculateSumAsync(IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds is not null) return (await _employeeService.GetByIdsAsync(
            employeeIds.ToArray())).Sum(x => x.Salary);

        return (await _employeeService.GetAllAsync()).Sum(x => x.Salary);
    }
}
