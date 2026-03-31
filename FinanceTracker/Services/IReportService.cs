using FinanceTracker.Api.Dtos;

namespace FinanceTracker.Api.Services
{
    public interface IReportService
    {
        Task<MonthlySummaryDto> GetMonthlySummaryAsync(Guid userId, int month, int year);
    }
}
