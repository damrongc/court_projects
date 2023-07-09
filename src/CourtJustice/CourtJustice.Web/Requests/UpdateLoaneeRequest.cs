namespace CourtJustice.Web.Requests
{
    public class UpdateLoaneeRequest
    {
        public string CusId { get; set; }
        public string Name { get; set; }
        public string HomeAddress1 { get; set; }
        public string HomeAddress2 { get; set; }
        public string HomeAddress3 { get; set; }
        public string HomeAddress4 { get; set; }
        public string TelephoneHome { get; set; }
        public string MobileHome { get; set; }
        public string IdenAddress1 { get; set; }
        public string IdenAddress2 { get; set; }
        public string IdenAddress3 { get; set; }
        public string IdenAddress4 { get; set; }
        public string MobileEmg { get; set; }

        public string CompanyName { get; set; }
        public string OccupationName { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public string OfficeAddress3 { get; set; }
        public string OfficeAddress4 { get; set; }
        public string TelephoneOffice { get; set; }
        public string MobileOffice { get; set; }
        //public int OccupationId { get; set; }
        public string EmployeeCode { get; set; }
        public string LoanTypeCode { get; set; }
        public int LoanTaskStatusId { get; set; }
    }
}
