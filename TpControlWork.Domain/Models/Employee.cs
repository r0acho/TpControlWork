using TpControlWork.Domain.Enums;
using TpControlWork.Domain.Models.Earnings;
using TpControlWork.Domain.Models.PaymentTypes;

namespace TpControlWork.Domain.Models;

public class Employee
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required EEmployeeType EmployeeType { get; set; }

    public required PaymentType PaymentType { get; set; }

    public required List<Earning> Earnings { get; set; }

    public decimal Salary { get
        {
            decimal baseSalary = PaymentType.CalculatePayment;
            decimal additionalEarnings = Earnings.Sum(earning => earning.CalculateEarnings);

            return baseSalary + additionalEarnings;
        }
    }
}

