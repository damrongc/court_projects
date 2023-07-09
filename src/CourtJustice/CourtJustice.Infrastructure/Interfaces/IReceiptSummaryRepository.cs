using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IReceiptSummaryRepository
    {
        Task AddOrUpdateReceipt(ReceiptSummary receipt);
        Task DeleteReceipt(ReceiptSummary receipt);
    }
}
