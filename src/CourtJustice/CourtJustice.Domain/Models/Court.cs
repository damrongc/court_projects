using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("court")]
    public class Court
    {
        [Key]
        [Display(Name = "รหัสหน่วยงาน")]
        public string CourtId { get; set; }
        [Display(Name = "หน่วยงานรับคำฟ้อง")]
        public string CourtName { get; set; }
    }
}
