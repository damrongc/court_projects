
namespace CourtJustice.Web.Requests
{
    public class JusticeCaseRequest
    {
        public string BlackCaseNo { get; set; } = string.Empty;
        public string CaseDate { get; set; } = string.Empty;
        public string ApprovalDate { get; set; } = string.Empty;
        public string JudgmentDate { get; set; } = string.Empty;
        public decimal AssetAmount { get; set; } = 0;
        public string CaseDocumentResult { get; set; } = string.Empty;
        public decimal FeeCase { get; set; } = 0;
        public string SubmissionDate { get; set; } = string.Empty;
        public string SubmissionResult { get; set; } = string.Empty;
        public string CommitDate { get; set; } = string.Empty;
        public string PostingDate { get; set; } = string.Empty;
        public string CourtId { get; set; } = string.Empty;
        public int CaseResultId { get; set; } = 0;
        public string CusId { get; set; } = string.Empty;
        public List<JusticeAppointmentRequest> JusticeAppointments { get; set; } = new();
        public List<JusticeLawyerRequest> JusticeLawyers { get; set; } = new();
    }
}
