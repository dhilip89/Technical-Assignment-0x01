using XYZ.Platform.Core.Application.Model;
using XYZ.Platform.Core.Domain.Model.Transactions;

namespace XYZ.Platform.Core.Application.Service
{
    public interface ITransactionService
    {
        TransactionsValidationResult ImportTransactionsFromCsv(string csvData);

        TransactionsValidationResult ImportTransactionsFromXml(string xmlData);

        IEnumerable<Transaction> GetAllTransactions();

        IEnumerable<Transaction> GetTransactionsByCurrencyCode(string currencyCode);

        IEnumerable<Transaction> GetTransactionsByDateRange(DateOnly fromDate, DateOnly toDate);

        IEnumerable<Transaction> GetTransactionsByStatus(TransactionStatus status);
    }
}
