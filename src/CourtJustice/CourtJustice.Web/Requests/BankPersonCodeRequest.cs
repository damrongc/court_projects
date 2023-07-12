namespace CourtJustice.Web.Requests
{
    public class BankPersonCodeRequest
    {
        public string EmployerCodeFilter { get; set; }
        public int BankActionId { get; set; }
        public int BankPersonId { get; set; }
        public string BankPersonCodeId { get; set; }
        public string BankPersonCodeName { get; set; }
    }
}
