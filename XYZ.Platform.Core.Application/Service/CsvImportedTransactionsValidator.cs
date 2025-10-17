using ISO._4217;
using XYZ.Platform.Core.Application.Model;

namespace XYZ.Platform.Core.Application.Service
{
    public sealed class CsvImportedTransactionsValidator : IImportedTransactionsValidator
    {
        public TransactionsValidationResult Validate(IEnumerable<ImportedTransaction> importedTransactions)
        {
            var validationResult = new TransactionsValidationResult();
            
            var validTransactions = new List<ImportedTransaction>();
            var invalidTransactions = new List<InvalidImportedTransaction>();

            foreach (var importedTransaction in importedTransactions)
            {
                var hasError = false;
                var validationFailedReasons = new List<ValidationFailedReason>();

                if (string.IsNullOrEmpty(importedTransaction.Id))
                {
                    hasError = true;

                    validationFailedReasons.Add(new ValidationFailedReason()
                    {
                        Field = nameof(ImportedTransaction.Id),
                        Detail = "Value cannot be empty."
                    });
                }

                if (importedTransaction.Id?.Length > 50)
                {
                    hasError = true;

                    validationFailedReasons.Add(new ValidationFailedReason()
                    {
                        Field = nameof(ImportedTransaction.Id),
                        Detail = "Value cannot be longer than 50 characters."
                    });
                }

                if (importedTransaction.Amount is null)
                {
                    hasError = true;

                    validationFailedReasons.Add(new ValidationFailedReason()
                    {
                        Field = nameof(ImportedTransaction.Amount),
                        Detail = "Value cannot be empty."
                    });
                }

                if (CurrencyCodesResolver.GetCurrenciesByCode(importedTransaction.CurrencyCode).Count() == 0)
                {
                    hasError = true;

                    validationFailedReasons.Add(new ValidationFailedReason()
                    {
                        Field = nameof(ImportedTransaction.CurrencyCode),
                        Detail = "Value must be a valid ISO-4217 currency code."
                    });
                }

                if (importedTransaction.Date is null)
                {
                    hasError = true;

                    validationFailedReasons.Add(new ValidationFailedReason()
                    {
                        Field = nameof(ImportedTransaction.Date),
                        Detail = "Value cannot be empty."
                    });
                }

                if (importedTransaction.Date is null)
                {
                    hasError = true;

                    validationFailedReasons.Add(new ValidationFailedReason()
                    {
                        Field = nameof(ImportedTransaction.Date),
                        Detail = "Value cannot be empty."
                    });
                }

                if (string.IsNullOrEmpty(importedTransaction.Status))
                {
                    hasError = true;

                    validationFailedReasons.Add(new ValidationFailedReason()
                    {
                        Field = nameof(ImportedTransaction.Status),
                        Detail = "Value cannot be empty."
                    });
                }

                if (!(importedTransaction.Status.Equals("Approved")
                    || importedTransaction.Status.Equals("Failed")
                    || importedTransaction.Status.Equals("Finished")))
                {
                    hasError = true;

                    validationFailedReasons.Add(new ValidationFailedReason()
                    {
                        Field = nameof(ImportedTransaction.Status),
                        Detail = "Value must be either: 'Approved', 'Failed', or 'Finished'."
                    });
                }

                if (hasError)
                {
                    validationResult.Success = false;

                    var invalidTransaction = new InvalidImportedTransaction()
                    {
                        Id = importedTransaction.Id,
                        Amount = importedTransaction.Amount,
                        CurrencyCode = importedTransaction.CurrencyCode,
                        Date = importedTransaction.Date,
                        Status = importedTransaction.Status,
                        Reasons = validationFailedReasons
                    };

                    invalidTransactions.Add(invalidTransaction);
                }
                else
                {
                    validTransactions.Add(importedTransaction);
                }
            }

            validationResult.ValidTransactions = validTransactions;
            validationResult.InvalidTransactions = invalidTransactions;

            return validationResult;
        }
    }
}
