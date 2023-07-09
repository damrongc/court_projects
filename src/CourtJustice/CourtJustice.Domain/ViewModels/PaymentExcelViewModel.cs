namespace CourtJustice.Domain.ViewModels
{
    public class PaymentExcelViewModel
    {
        public string EmployerCode { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string CusId { get; set; }
        public string CusName { get; set; }
        public string ContractNo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal WOBalance { get; set; }
        public string StartOverdueStatus { get; set; }
        public string EndOverdueStatus { get; set; }
        public  string UserCreated { get; set; }
    }
}
