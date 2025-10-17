namespace XYZ.Platform.Core.Domain.Model.Transactions
{
    public sealed class Transaction
    {
        public required string Id { get; set; }

        public required DateTime Date { get; set; }

        public required PaymentDetails PaymentDetails { get; set; }

        public required TransactionStatus Status { get; set; }
    }
}
