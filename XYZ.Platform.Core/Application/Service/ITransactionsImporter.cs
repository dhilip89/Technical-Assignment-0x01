using XYZ.Platform.Core.Application.Model;

namespace XYZ.Platform.Core.Application.Service
{
    public interface ITransactionsImporter
    {
        IEnumerable<ImportedTransaction> Import(string rawData);
    }
}
