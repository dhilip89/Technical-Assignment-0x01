using Microsoft.Extensions.Logging;
using XYZ.Platform.Core.Application.Model;
using XYZ.Platform.Core.Domain.Model.Transactions;
using XYZ.Platform.Core.Domain.Repository;

namespace XYZ.Platform.Core.Application.Service
{
    public sealed class TransactionService : ITransactionService
    {
        public required IImportedTransactionsValidator CsvImportedTransactionsValidator { get; init; }
        
        public required ITransactionsImporter CsvTransactionsImporter { get; init; }
        
        public required IImportedTransactionsValidator XmlImportedTransactionsValidator { get; init; }

        public required ITransactionsImporter XmlTransactionsImporter { get; init; }

        public required ITransactionsRepository TransactionsRepository { get; init; }

        private ILogger Logger { get; init; }

        public TransactionService(ILogger logger) 
        {
            Logger = logger;
        }

        public TransactionsValidationResult ImportTransactionsFromCsv(string csvData)
        {
            var importedTransactions = CsvTransactionsImporter.Import(csvData);

            var validationResult = CsvImportedTransactionsValidator.Validate(importedTransactions);

            if (validationResult.Success)
            {
                TransactionsRepository.Store(ImportedTransaction.Remap(importedTransactions));
            }

            return validationResult;
        }

        public TransactionsValidationResult ImportTransactionsFromXml(string xmlData)
        {
            var importedTransactions = XmlTransactionsImporter.Import(xmlData);

            var validationResult = XmlImportedTransactionsValidator.Validate(importedTransactions);

            if (validationResult.Success)
            {
                TransactionsRepository.Store(ImportedTransaction.Remap(importedTransactions));
            }

            return validationResult;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return TransactionsRepository.GetAll();
        }

        public IEnumerable<Transaction> GetTransactionsByCurrencyCode(string currencyCode)
        {
            return TransactionsRepository.GetByCurrencyCode(currencyCode);
        }

        public IEnumerable<Transaction> GetTransactionsByDateRange(DateOnly fromDate, DateOnly toDate)
        {
            return TransactionsRepository.GetByDateRange(fromDate, toDate);
        }

        public IEnumerable<Transaction> GetTransactionsByStatus(TransactionStatus status)
        {
            return TransactionsRepository.GetByStatus(status);
        }
    }
}
