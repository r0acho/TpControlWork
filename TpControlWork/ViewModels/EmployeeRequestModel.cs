using System.ComponentModel.DataAnnotations;
using TpControlWork.Domain.Enums;

namespace TpControlWork.ViewModels;

public class EmployeeRequestModel
{
    [Required]
    public string? Name { get; set; } = null!;

    [Required]
    public EEmployeeType? EmployeeType { get; set; }

    [Required]
    public PaymentTypeRequestModel? PaymentType { get; set; } = null!;

    public List<EarningRequestModel> Earnings { get; set; } = null!;
}
