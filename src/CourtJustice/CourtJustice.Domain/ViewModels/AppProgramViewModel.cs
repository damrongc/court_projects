namespace CourtJustice.Domain.ViewModels
{
    public class AppProgramViewModel
    {
        public int PermissionId { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int? ParentProgramId { get; set; }
        public bool IsCheck { get; set; }
        public List<AppProgramViewModel> ProgramParents { get; set; }
    }
}
