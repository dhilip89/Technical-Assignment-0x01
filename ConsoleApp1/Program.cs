using System.Diagnostics;
using System.Globalization;
using U8Xml;
using XYZ.Platform.Core.Application.Model;
using XYZ.Platform.Core.Application.Service;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var csv = @"""Invoice0000001"",1000.00,""USD"",""20/02/2019 12:33:16"",""Approved""
""Invoice0000002"",300.00,""USD"",""21/02/2019 02:04:59"",""Failed""
";

            var xml = @"
<Transactions>
    <Transaction id=""Inv00001"">
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
    <Transaction id=""Inv00002"">
        <TransactionDate>2019-01-24T16:09:15</TransactionDate>
        <PaymentDetails>
            <Amount>10000.00</Amount>
            <CurrencyCode>EUR</CurrencyCode>
        </PaymentDetails>
        <Status>Rejected</Status>
    </Transaction>
</Transactions>
";

            var dt = DateTime.ParseExact("20/02/2019 12:33:16", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var validator = new XmlImportedTransactionsValidator();
            // Debugger.Break();

            try
            {



                var csvImporter = new CsvTransactionsImporter();
                var xmlImporter = new XmlTransactionsImporter();

                var csvResult = csvImporter.Import(csv);
                var xmlResult = xmlImporter.Import(xml);

                var aaa = validator.Validate(csvResult);
                var bbb = validator.Validate(xmlResult);

                using var xml0 = XmlParser.Parse(xml);

                var root = xml0.Root;

                foreach (var transactionNode in root.Children)
                {
                    var importedTransaction = new ImportedTransaction();

                    importedTransaction.Id = transactionNode.FindAttributeOrDefault("id").Value.Value.ToString();
                    importedTransaction.Date = DateTime.ParseExact(transactionNode.FindChildOrDefault("TransactionDate").Value.FirstChild.ToString(), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                    importedTransaction.Status = transactionNode.FindChildOrDefault("Status").Value.FirstChild.ToString();

                    var paymentDetailsNode = transactionNode.FindChildOrDefault("PaymentDetails").Value;

                    importedTransaction.Amount = decimal.Parse(paymentDetailsNode.FindChildOrDefault("Amount").Value.FirstChild.ToString());
                    importedTransaction.CurrencyCode = paymentDetailsNode.FindChildOrDefault("CurrencyCode").Value.FirstChild.ToString();

                    //Debugger.Break();
                }

                Debugger.Break();

            }
            catch (Exception e)
            {
                Debugger.Break();
            }

            Console.Read();
        }
    }
}
