using XYZ.Platform.Core.Domain.Model.Transactions;

namespace XYZ.Platform.Core.Application.Model
{
    public sealed class ImportedTransaction
    {
        public static TransactionStatus Remap(string transactionStatus)
        {
            switch (transactionStatus)
            {
                case "Approved":
                    return TransactionStatus.Approved;
                case "Failed":
                case "Rejected":
                    return TransactionStatus.Rejected;
                case "Finished":
                case "Done":
                    return TransactionStatus.Done;
                default:
                    return TransactionStatus.Undefined;
            }
        }

        public static IEnumerable<Transaction> Remap(IEnumerable<ImportedTransaction> importedTransactions)
        {
            foreach (var importedTransaction in importedTransactions)
            {
                yield return new Transaction()
                {
                    Id = importedTransaction.Id!,
                    Date = (DateTime)importedTransaction.Date!,
                    PaymentDetails = new PaymentDetails() { Amount = (decimal)importedTransaction.Amount!, CurrencyCode = importedTransaction.CurrencyCode! },
                    Status = Remap(importedTransaction.Status!)
                };
            }
        }

        public string? Id { get; set; }

        public decimal? Amount { get; set; }

        public string? CurrencyCode { get; set; }

        public DateTime? Date { get; set; }

        public string? Status { get; set; }
    }
}
