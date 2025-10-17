namespace XYZ.Platform.Core.Application.Model
{
    public sealed class TransactionsValidationResult
    {
        public bool Success { get; set; } = true;

        public IEnumerable<ImportedTransaction> ValidTransactions { get; set; } = [];

        public IEnumerable<InvalidImportedTransaction> InvalidTransactions { get; set; } = [];
    }
}
