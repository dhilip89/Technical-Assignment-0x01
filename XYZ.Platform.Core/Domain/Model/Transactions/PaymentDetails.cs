namespace XYZ.Platform.Core.Domain.Model.Transactions
{
    public sealed class PaymentDetails
    {
        public required decimal Amount { get; set; }

        public required string CurrencyCode { get; set; }
    }
}
