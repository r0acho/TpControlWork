using Microsoft.AspNetCore.Mvc;
using TpControlWork.Services.Implementations.CalculateStrategies;
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
        _statisticsCalculatorService.CalculateStrategy = new CalculateSumStrategy();
        return await _statisticsCalculatorService.CalculateByStrategyAsync(employeeIds);
    }

    // GET: api/statisticsCalculator/avg
    [HttpGet]
    [Route("avg")]
    public async Task<decimal> GetSalaryAvg([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        _statisticsCalculatorService.CalculateStrategy = new CalculateAverageStrategy();
        return await _statisticsCalculatorService.CalculateByStrategyAsync(employeeIds);
    }

    // GET: api/statisticsCalculator/median
    [HttpGet]
    [Route("median")]
    public async Task<decimal> GetSalaryMedian([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        _statisticsCalculatorService.CalculateStrategy = new CalculateMedianStrategy();
        return await _statisticsCalculatorService.CalculateByStrategyAsync(employeeIds);
    }

    // GET: api/statisticsCalculator/min
    [HttpGet]
    [Route("min")]
    public async Task<decimal> GetSalaryMin([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        _statisticsCalculatorService.CalculateStrategy = new CalculateMinStrategy();
        return await _statisticsCalculatorService.CalculateByStrategyAsync(employeeIds);
    }

    // GET: api/statisticsCalculator/max
    [HttpGet]
    [Route("max")]
    public async Task<decimal> GetSalaryMax([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        _statisticsCalculatorService.CalculateStrategy = new CalculateMaxStrategy();
        return await _statisticsCalculatorService.CalculateByStrategyAsync(employeeIds);
    }
}
