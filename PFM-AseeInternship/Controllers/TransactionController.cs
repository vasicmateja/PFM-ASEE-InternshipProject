using Microsoft.AspNetCore.Mvc;
using PFM_AseeInternship.Models;
using PFM_AseeInternship.Services;

namespace PFM_AseeInternship.Controllers
{
    [Route("v1/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        TransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger, TransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAsync([FromQuery] string transactionKind, [FromQuery] string? startDate, [FromQuery] string? endDate
            , [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? sortBy    = null, [FromQuery] SortOrder sortOrder = SortOrder.asc) 
        {
           // page = 1;
           // pageSize = 10;
            _logger.LogInformation("Returning {page}. of transactions", page);
            var result = await _transactionService.GetTransactions(transactionKind, startDate, endDate, page, pageSize, sortBy, sortOrder);
            
            return Ok(result);
        }


        [HttpPost("import")]
        public IActionResult importTransactions()
        {
            _transactionService.ImportTransactions();
            return Ok();
        }

        [HttpPost("{id}/split")]
        public IActionResult splitTransaction()
        {
            return Ok();
        }

        [HttpPost("{transactionId}/categorize")]
        public IActionResult categorizeTransaction([FromRoute] int transactionId)
        {
            _transactionService.CategorizeTransaction(transactionId);
            return Ok();
        }

        [HttpPost("autoCategorize")]
        public IActionResult autoCategorizeTransactions()
        {
            _transactionService.AutoCategorize();
            return Ok();
            
        }


    }
}