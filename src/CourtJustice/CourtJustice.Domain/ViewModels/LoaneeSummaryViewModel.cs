namespace CourtJustice.Domain.ViewModels
{
    public class LoaneeSummaryViewModel
    {
        public string YearMonth { get; set; }
        public string EmployerCode { get; set; }
        public string EmployerName { get; set; }
        public string MonthName { get; set; }
        public int LoaneeCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Target { get; set; }
    }
}
