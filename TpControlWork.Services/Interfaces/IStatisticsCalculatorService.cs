namespace TpControlWork.Services.Interfaces;

public interface IStatisticsCalculatorService
{
    Task<decimal> CalculateSumAsync(IEnumerable<int>? employeeIds = null);

    Task<decimal> CalculateAverageAsync(IEnumerable<int>? employeeIds = null);

    Task<decimal> CalculateMedianAsync(IEnumerable<int>? employeeIds = null);

    Task<decimal> CalculateMaxAsync(IEnumerable<int>? employeeIds = null);

    Task<decimal> CalculateMinAsync(IEnumerable<int>? employeeIds = null);
}
