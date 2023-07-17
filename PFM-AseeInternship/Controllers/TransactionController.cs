using Microsoft.AspNetCore.Mvc;
using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.Controllers
{
    [ApiController]
    [Route("v1/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTransactions([FromQuery] string transactionKind, [FromQuery] string? startDate, [FromQuery] string? endDate, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? sortBy = null, [FromQuery] SortOrder sortOrder = SortOrder.asc) 
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult importTransactions()
        {
            return Ok();
        }

        [HttpPost("{id}/split")]
        public IActionResult splitTransaction()
        {
            return Ok();
        }

        [HttpPost("{id}/categorize")]
        public IActionResult categorizeTransaction()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult autoCategorizeTransactions()
        {
            return Ok();
        }


    }
}