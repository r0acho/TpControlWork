using System.ComponentModel.DataAnnotations;
using TpControlWork.Enums;

namespace TpControlWork.ViewModels;

public class PaymentTypeRequestModel
{
    [Required]
    public EPaymentType? PaymentType { get; set; }

    public decimal? HourlyRate { get; set; }

    public int? HoursWorked { get; set; }

    public decimal? RatePerPiece { get; set; }

    public int? NumberOfPieces { get; set; }

    public decimal? MonthlySalary { get; set; }
}
