namespace CourtJustice.Domain.ViewModels
{
    public class ReceiptSummaryViewModel
    {
        public int Id { get; set; }
        public string EmployerCode { get; set; }
        public int ReceiptMonth { get; set; }
        public int ReceiptYear { get; set; }
        public string CollectorId { get; set; }
        public decimal TotalAmont { get; set; }
        public decimal Fee { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}
