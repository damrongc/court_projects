namespace CourtJustice.Domain.ViewModels
{
    public class LoaneeNoticeViewModel
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string NoticeDate { get; set; }
        public string LoaneeName { get; set; }
        public string LoaneeNumber { get; set; }
        public string ContractDate { get; set; }
        public double Amount { get; set; }
        public double Rate { get; set; }
        public double DebtAmount { get; set; }
        public double Fee { get; set; }
        public double TotalAmount { get; set; }

    }
}
