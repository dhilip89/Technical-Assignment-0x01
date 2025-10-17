using XYZ.Platform.Core.Application.Model;

namespace XYZ.Platform.Core.Application.Service
{
    public interface IImportedTransactionsValidator
    {
       TransactionsValidationResult Validate(IEnumerable<ImportedTransaction> importedTransactions);
    }
}
