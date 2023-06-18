namespace CourtJustice.Domain.ViewModels
{
    public class UserPermissionViewModel
    {
        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ProgramId { get; set; }
        public string State { get; set; }
        public List<AppProgramViewModel> AppPrograms { get; set; }
    }
}
