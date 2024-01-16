using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpControlWork.DataAccess.Entities;

public class PaymentType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    public decimal? HourlyRate { get; set; }

    public int? HoursWorked { get; set; }

    public decimal? MonthlySalary { get; set; }

    public decimal? RatePerPiece { get; set; }

    public int? NumberOfPieces { get; set; }
}
