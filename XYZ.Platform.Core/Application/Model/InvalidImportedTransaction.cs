namespace XYZ.Platform.Core.Application.Model
{
    public sealed class InvalidImportedTransaction
    {
        public string? Id { get; set; }

        public decimal? Amount { get; set; }

        public string? CurrencyCode { get; set; }

        public DateTime? Date { get; set; }

        public string? Status { get; set; }

        public IEnumerable<ValidationFailedReason> Reasons { get; set; } = [];
    }
}
