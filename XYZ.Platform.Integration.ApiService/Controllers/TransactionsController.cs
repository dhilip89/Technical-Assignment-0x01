using Microsoft.AspNetCore.Mvc;
using System.Text;
using XYZ.Platform.Core.Application.Service;
using XYZ.Platform.Core.Domain.Model.Transactions;
using XYZ.Platform.Integration.ApiService.Models;

namespace XYZ.Platform.Integration.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        private string ReadUploadedFile(IFormFile file)
        {
            using var memoryStream = new MemoryStream();

            file.CopyTo(memoryStream);

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        [HttpPost]
        [Route("import")]
        [RequestSizeLimit(1 * 1024 * 1024)]
        public ActionResult Import(IFormFile file)
        {
            if (file.ContentType.Equals("text/csv", StringComparison.Ordinal))
            {
                var data = ReadUploadedFile(file);
                var validationResult = _transactionService.ImportTransactionsFromCsv(data);

                if (validationResult.Success)
                {
                    return Ok();
                } 
                else
                {
                    return BadRequest(validationResult);
                }
            }
            else if (file.ContentType.Equals("text/xml", StringComparison.Ordinal))
            {
                var data = ReadUploadedFile(file);
                var validationResult = _transactionService.ImportTransactionsFromXml(data);

                if (validationResult.Success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(validationResult.InvalidTransactions);
                }
            }
            else
            {
                return BadRequest("Unknown format");
            }
        }

        [HttpGet]
        [Route("all")]
        public ActionResult GetAll()
        {
            var dto = TransactionDto.Remap(_transactionService.GetAllTransactions());

            return Ok(dto);
        }

        [HttpGet]
        [Route("by_currency")]
        public ActionResult GetByCurrency([FromQuery] string currency)
        {
            var dto = TransactionDto.Remap(_transactionService.GetTransactionsByCurrencyCode(currency));

            return Ok(dto);
        }

        [HttpGet]
        [Route("by_date")]
        public ActionResult GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var dto = TransactionDto.Remap(_transactionService.GetTransactionsByDateRange(DateOnly.FromDateTime(from), DateOnly.FromDateTime(to)));

            return Ok(dto);
        }

        [HttpGet]
        [Route("by_status")]
        public ActionResult GetByStatus([FromQuery] string status)
        {
            var transactionStatus = TransactionStatus.Undefined;

            if (status.Equals("A", StringComparison.OrdinalIgnoreCase))
            {
                transactionStatus = TransactionStatus.Approved;
            }
            else if (status.Equals("R", StringComparison.OrdinalIgnoreCase))
            {
                transactionStatus = TransactionStatus.Rejected;
            } 
            else if (status.Equals("D", StringComparison.OrdinalIgnoreCase))
            {
                transactionStatus = TransactionStatus.Done;
            }

            var dto = TransactionDto.Remap(_transactionService.GetTransactionsByStatus(transactionStatus));

            return Ok(dto);
        }
    }
}
