namespace CourtJustice.Domain.ViewModels
{
    public class LoaneeRemarkExcelViewModel
    {
        public string EmployerCode { get; set; }
        public string AssignDate { get; set; }
        public string ExpireDate { get; set; }
        public DateTime TransactionDatetime { get; set; }
        public string CusId { get; set; }
        public string NationalityId { get; set; }
        public string CusName { get; set; }
        public string ContractNo { get; set; }
        public string BankActionCodeId { get; set; }
        public string BankActionCodeName { get; set; }
        public string BankResultCodeId { get; set; }
        public string BankResultCodeName { get; set; }
        public string CompanyActionCodeId { get; set; }
        public string CompanyActionCodeName { get; set; }
        public string CompanyResultCodeId { get; set; }
        public string CompanyResultCodeName { get; set; }
        public string BankPersonCodeId { get; set; }
        public string BankPersonCodeName { get; set; }
        public string Remark { get; set; }
        public string FollowContractNo { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentContract { get; set; }
        public decimal Amount { get; set; }
        public string Collector { get; set; }
        public int BankActionId { get; set; }
        public int BankResultId { get; set; }
        public int BankPersonId { get; set; }
        public int CompanyActionId { get; set; }
        public int CompanyResultId { get; set; }
        public string EmployerWorkGroup { get; set; }

    }
}
