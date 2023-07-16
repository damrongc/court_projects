namespace CourtJustice.Web.Requests
{
    public class CompanyResultCodeRequest
    {
        public int CompanyResultId { get; set; }
        public string CompanyResultCodeId { get; set; }
        public string CompanyResultCodeName { get; set; }
        public bool NotCallFlag { get; set; }
        public bool ShowHideFlag { get; set; }
        public int CompanyActionId { get; set; }
    }
}
