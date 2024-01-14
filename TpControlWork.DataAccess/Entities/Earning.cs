using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TpControlWork.DataAccess.Entities;

public class Earning
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    public decimal? BonusAmount { get; set; }

    public decimal? OvertimeRate { get; set; }

    public int? OvertimeHours { get; set; }

    public int FkEmployeeId { get; set; }

    public Employee FkEmployee { get; set; } = null!;
}
