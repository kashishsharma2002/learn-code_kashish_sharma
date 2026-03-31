using FinanceTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetMonthlySummary(
            [FromQuery] Guid userId,
            [FromQuery] int month,
            [FromQuery] int year)
        {
            var summary = await _reportService.GetMonthlySummaryAsync(userId, month, year);
            return Ok(summary);
        }
    }
}
