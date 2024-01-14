namespace TpControlWork.Domain.Models.PaymentTypes;

public class PieceRatePayment : PaymentType
{
    public required decimal RatePerPiece { get; set; }

    public required int NumberOfPieces { get; set; }

    public override decimal CalculatePayment => RatePerPiece * NumberOfPieces;
}
