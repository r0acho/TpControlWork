namespace TpControlWork.Domain.Models.Earnings;

public class BonusEarnings : Earning
{
    public override decimal CalculateEarnings => BonusAmount;

    public required decimal BonusAmount { get; set; }
}
