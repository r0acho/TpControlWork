using System.ComponentModel.DataAnnotations;
using TpControlWork.Enums;

namespace TpControlWork.ViewModels;

public class EarningRequestModel
{
    [Required]
    public EEarningType? Type { get; set; }

    public decimal? OvertimeRate { get; set; }

    public int? OvertimeHours { get; set; }

    public decimal? BonusAmount { get; set; }
}
