using Microsoft.AspNetCore.Mvc;
using TpControlWork.Services.Interfaces;

namespace TpControlWork.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsCalculatorController : ControllerBase
{
    private readonly IStatisticsCalculatorService _statisticsCalculatorService;

    public StatisticsCalculatorController(IStatisticsCalculatorService statisticsCalculatorService)
    {
        _statisticsCalculatorService = statisticsCalculatorService;
    }

    // GET: api/statisticsCalculator/sum
    [HttpGet]
    [Route("sum")]
    public async Task<decimal> GetSalarySum([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds == null) return await _statisticsCalculatorService.CalculateSumAsync();

        return await _statisticsCalculatorService.CalculateSumAsync(employeeIds.ToArray());
    }

    // GET: api/statisticsCalculator/avg
    [HttpGet]
    [Route("avg")]
    public async Task<decimal> GetSalaryAvg([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds == null) return await _statisticsCalculatorService.CalculateAverageAsync();

        return await _statisticsCalculatorService.CalculateAverageAsync(employeeIds.ToArray());
    }

    // GET: api/statisticsCalculator/median
    [HttpGet]
    [Route("median")]
    public async Task<decimal> GetSalaryMedian([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds == null) return await _statisticsCalculatorService.CalculateMedianAsync();

        return await _statisticsCalculatorService.CalculateMedianAsync(employeeIds.ToArray());
    }

    // GET: api/statisticsCalculator/min
    [HttpGet]
    [Route("min")]
    public async Task<decimal> GetSalaryMin([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds == null) return await _statisticsCalculatorService.CalculateMinAsync();

        return await _statisticsCalculatorService.CalculateMinAsync(employeeIds.ToArray());
    }

    // GET: api/statisticsCalculator/max
    [HttpGet]
    [Route("max")]
    public async Task<decimal> GetSalaryMax([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds == null) return await _statisticsCalculatorService.CalculateMaxAsync();

        return await _statisticsCalculatorService.CalculateMaxAsync(employeeIds.ToArray());
    }
}
