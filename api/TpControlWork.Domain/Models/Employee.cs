using TpControlWork.Domain.Enums;
using TpControlWork.Domain.Models.Earnings;
using TpControlWork.Domain.Models.PaymentTypes;

namespace TpControlWork.Domain.Models;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public EEmployeeType EmployeeType { get; set; }

    public PaymentType PaymentType { get; set; } = null!;

    public List<Earning> Earnings { get; set; } = null!; 

    public decimal Salary { get
        {
            decimal baseSalary = PaymentType.CalculatePayment;
            decimal additionalEarnings = Earnings.Sum(earning => earning.CalculateEarnings);

            return baseSalary + additionalEarnings;
        }
    }
}

