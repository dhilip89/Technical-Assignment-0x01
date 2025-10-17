using nietras.SeparatedValues;
using System.Globalization;
using XYZ.Platform.Core.Application.Model;

namespace XYZ.Platform.Core.Application.Service
{
    public sealed class CsvTransactionsImporter : ITransactionsImporter
    {
        public IEnumerable<ImportedTransaction> Import(string rawData)
        {
            using var reader = Sep.New(',').Reader(o => o with { HasHeader = false, Unescape = true }).FromText(rawData);
            
            foreach (var readRow in reader)
            {
                var importedTransaction = new ImportedTransaction()
                {
                    Id = readRow[0].ToString(),
                    Amount = readRow[1].Parse<decimal>(),
                    CurrencyCode = readRow[2].ToString(),
                    Date = DateTime.ParseExact(readRow[3].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    Status = readRow[4].ToString()
                };

                yield return importedTransaction;
            }
        }
    }
}
