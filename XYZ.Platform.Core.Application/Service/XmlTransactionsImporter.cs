using System.Globalization;
using U8Xml;
using XYZ.Platform.Core.Application.Model;

namespace XYZ.Platform.Core.Application.Service
{
    public sealed class XmlTransactionsImporter : ITransactionsImporter
    {
        public IEnumerable<ImportedTransaction> Import(string rawData)
        {
            using var xml = XmlParser.Parse(rawData);

            var rootNode = xml.Root;

            foreach (var transactionNode in rootNode.Children)
            {
                var importedTransaction = new ImportedTransaction();

                importedTransaction.Id = transactionNode.FindAttributeOrDefault("id").Value.Value.ToString();
                importedTransaction.Date = DateTime.ParseExact(transactionNode.FindChildOrDefault("TransactionDate").Value.FirstChild.ToString(), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                importedTransaction.Status = transactionNode.FindChildOrDefault("Status").Value.FirstChild.ToString();

                var paymentDetailsNode = transactionNode.FindChildOrDefault("PaymentDetails").Value;

                importedTransaction.Amount = decimal.Parse(paymentDetailsNode.FindChildOrDefault("Amount").Value.FirstChild.ToString());
                importedTransaction.CurrencyCode = paymentDetailsNode.FindChildOrDefault("CurrencyCode").Value.FirstChild.ToString();

                yield return importedTransaction;
            }
        }
    }
}
