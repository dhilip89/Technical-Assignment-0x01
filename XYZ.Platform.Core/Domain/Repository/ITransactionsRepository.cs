using XYZ.Platform.Core.Domain.Model.Transactions;

namespace XYZ.Platform.Core.Domain.Repository
{
    public interface ITransactionsRepository
    {
        bool Store(IEnumerable<Transaction> transactions);

        IEnumerable<Transaction> GetAll();

        IEnumerable<Transaction> GetByCurrencyCode(string currencyCode);

        IEnumerable<Transaction> GetByDateRange(DateOnly fromDate, DateOnly toDate);

        IEnumerable<Transaction> GetByStatus(TransactionStatus status);
    }
}
