using System.Collections.Concurrent;
using XYZ.Platform.Core.Domain.Model.Transactions;
using XYZ.Platform.Core.Domain.Repository;

namespace XYZ.Platform.Infrastructure.Repository
{
    // In-memory database
    // For quick demo purpose only, never use this kind of implementation in a real system.

    public sealed class InMemoryTransactionsRepository : ITransactionsRepository
    {
        private readonly ConcurrentDictionary<string, Transaction> _storage = new ConcurrentDictionary<string, Transaction>();

        public IEnumerable<Transaction> GetAll()
        {
            foreach (var transaction in _storage.Values)
            {
                yield return transaction;
            }
        }

        public IEnumerable<Transaction> GetByCurrencyCode(string currencyCode)
        {
            foreach (var transaction in _storage.Values)
            {
                if (transaction.PaymentDetails.CurrencyCode.Equals(currencyCode, StringComparison.Ordinal))
                {
                    yield return transaction;
                }
            }
        }

        public IEnumerable<Transaction> GetByDateRange(DateOnly fromDate, DateOnly toDate)
        {
            foreach (var transaction in _storage.Values)
            {
                var transactionDate = DateOnly.FromDateTime(transaction.Date);

                if (transactionDate >= fromDate && transactionDate <= toDate)
                {
                    yield return transaction;
                }
            }
        }

        public IEnumerable<Transaction> GetByStatus(TransactionStatus status)
        {
            foreach (var transaction in _storage.Values)
            {
                if (transaction.Status == status)
                {
                    yield return transaction;
                }
            }
        }

        public bool Store(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                _storage[transaction.Id] = transaction;
            }

            return true;
        }
    }
}
