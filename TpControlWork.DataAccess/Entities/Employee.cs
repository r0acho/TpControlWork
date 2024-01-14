using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TpControlWork.DataAccess.Entities;

public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int FkEmployeeTypeId { get; set; }

    public int FkPaymentTypeId { get; set; }

    public EmployeeType FkEmployeeType { get; set; } = null!;

    public PaymentType FkPaymentType { get; set; } = null!;

    public ICollection<Earning> Earnings { get; set; } = null!;
}
