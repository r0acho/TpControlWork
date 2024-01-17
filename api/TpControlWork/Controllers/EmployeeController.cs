using Microsoft.AspNetCore.Mvc;
using TpControlWork.Domain.Models;
using TpControlWork.Domain.Models.Earnings;
using TpControlWork.Domain.Models.PaymentTypes;
using TpControlWork.Enums;
using TpControlWork.Services.Interfaces;
using TpControlWork.ViewModels;

namespace TpControlWork.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // POST: api/employee
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequestModel employeeRequest)
    {
        var employee = new Employee
        {
            Name = employeeRequest.Name!,
            EmployeeType = employeeRequest.EmployeeType!.Value,
            PaymentType = MapPaymentType(employeeRequest.PaymentType!),
            Earnings = MapEarnings(employeeRequest.Earnings)
        };

        await _employeeService.CreateAsync(employee);

        return Ok("Employee created successfully");
    }

    // GET: api/employee
    [HttpGet]
    public async Task<IEnumerable<Employee>> GetEmployees([FromQuery] IEnumerable<int>? employeeIds = null)
    {
        if (employeeIds == null) return await _employeeService.GetAllAsync();

        return await _employeeService.GetByIdsAsync(employeeIds.ToArray());
    }

    // PUT: api/employee
    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeRequestModel employeeRequest)
    {
        var employee = new Employee
        {
            Name = employeeRequest.Name!,
            EmployeeType = employeeRequest.EmployeeType!.Value,
            PaymentType = MapPaymentType(employeeRequest.PaymentType!),
            Earnings = MapEarnings(employeeRequest.Earnings)
        };

        await _employeeService.UpdateAsync(employee);

        return NoContent();
    }

    // DELETE: api/employee
    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee([FromQuery] int employeeId)
    {
        await _employeeService.DeleteAsync(employeeId);
        return NoContent();
    }

    private PaymentType MapPaymentType(PaymentTypeRequestModel paymentTypeRequest)
    {
        return paymentTypeRequest.PaymentType switch
        {
            Enums.EPaymentType.Hourly => new HourlyPayment
            {
                HourlyRate = paymentTypeRequest.HourlyRate
                    ?? throw new ArgumentNullException(nameof(paymentTypeRequest.HourlyRate)),
                HoursWorked = paymentTypeRequest.HoursWorked
                    ?? throw new ArgumentNullException(nameof(paymentTypeRequest.HoursWorked))
            },
            Enums.EPaymentType.Salary => new SalaryPayment 
            { 
                MonthlySalary = paymentTypeRequest.MonthlySalary
                    ?? throw new ArgumentNullException(nameof(paymentTypeRequest.MonthlySalary))
            },
            Enums.EPaymentType.PieceRate => new PieceRatePayment 
            { 
                NumberOfPieces = paymentTypeRequest.NumberOfPieces
                    ?? throw new ArgumentNullException(nameof(paymentTypeRequest.NumberOfPieces)),
                RatePerPiece = paymentTypeRequest.RatePerPiece
                    ?? throw new ArgumentNullException(nameof(paymentTypeRequest.RatePerPiece)),
            },
            _ => throw new InvalidOperationException()
        };
    }

    private List<Earning> MapEarnings(List<EarningRequestModel> earningsRequest)
    {
        return earningsRequest.Select<EarningRequestModel, Earning>(earningRequest =>
        {
            return earningRequest.Type switch
            {
                EEarningType.Overtime => new OvertimeEarnings
                {
                    OvertimeHours = earningRequest.OvertimeHours
                        ?? throw new ArgumentNullException(nameof(earningRequest.OvertimeHours)),
                    OvertimeRate = earningRequest.OvertimeRate
                        ?? throw new ArgumentNullException(nameof(earningRequest.OvertimeRate)),
                },
                EEarningType.Bonus => new BonusEarnings
                {
                    BonusAmount = earningRequest.BonusAmount
                        ?? throw new ArgumentNullException(nameof(earningRequest.BonusAmount))
                },
                _ => throw new ArgumentException("Не определен тип вознаграждения")
            };
        }).ToList();
    }
}