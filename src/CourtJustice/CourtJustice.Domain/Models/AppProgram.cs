using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("app_program")]
    public class AppProgram
    {
        [Key]
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int? ParentProgramId { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? ProgramParam { get; set; }
        public string? MenuIcon { get; set; }


    }
}
