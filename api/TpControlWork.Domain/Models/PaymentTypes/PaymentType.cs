namespace TpControlWork.Domain.Models.PaymentTypes;

abstract public class PaymentType
{
    abstract public decimal CalculatePayment { get; }
}

