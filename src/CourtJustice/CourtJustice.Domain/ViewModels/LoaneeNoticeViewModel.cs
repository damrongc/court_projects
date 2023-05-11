namespace CourtJustice.Domain.ViewModels
{
    public class LoaneeNoticeViewModel
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string NoticeDate { get; set; }
        public string LoaneeName { get; set; }
        public string LoaneeNumber { get;}
        public string ContractDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal DebtAmount { get; set; }
        public decimal Fee { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
