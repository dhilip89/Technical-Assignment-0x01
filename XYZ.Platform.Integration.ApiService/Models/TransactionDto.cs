using XYZ.Platform.Core.Domain.Model.Transactions;

namespace XYZ.Platform.Integration.ApiService.Models
{
    public sealed class TransactionDto
    {
        public static string Remap(TransactionStatus transactionStatus)
        {
            if (transactionStatus == TransactionStatus.Approved) return "A";
            if (transactionStatus == TransactionStatus.Rejected) return "R";
            if (transactionStatus == TransactionStatus.Rejected) return "D";

            return "";
        }

        public static IEnumerable<TransactionDto> Remap(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                yield return new TransactionDto()
                {
                    id = transaction.Id,
                    payment = $"{transaction.PaymentDetails.Amount} {transaction.PaymentDetails.CurrencyCode}",
                    status = Remap(transaction.Status)
                };
            }
        }

        public string? id { get; set; }

        public string? payment { get; set; }

        public string? status { get; set; }
    }
}
