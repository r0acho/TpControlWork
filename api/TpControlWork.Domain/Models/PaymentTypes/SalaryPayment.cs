namespace TpControlWork.Domain.Models.PaymentTypes;

public class SalaryPayment : PaymentType
{
    public required decimal MonthlySalary { get; set; }

    public override decimal CalculatePayment => MonthlySalary;
}
