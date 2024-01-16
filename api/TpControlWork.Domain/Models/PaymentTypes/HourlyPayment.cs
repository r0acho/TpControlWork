namespace TpControlWork.Domain.Models.PaymentTypes;

public class HourlyPayment : PaymentType
{
    public override decimal CalculatePayment => HourlyRate * HoursWorked;

    public required decimal HourlyRate { get; set; }

    public required int HoursWorked { get; set; }
}
