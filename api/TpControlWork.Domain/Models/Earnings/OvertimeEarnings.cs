namespace TpControlWork.Domain.Models.Earnings;

public class OvertimeEarnings : Earning
{
    public decimal OvertimeRate { get; set; }

    public int OvertimeHours { get; set; }

    public override decimal CalculateEarnings => OvertimeRate * OvertimeHours;
    
}
