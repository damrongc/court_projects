using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("employer")]
    public class Employer : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสผู้ว่าจ้าง")]
        public string EmployerCode { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ผู้ว่าจ้าง")]
        public string EmployerName { get; set; } = string.Empty;
    }
}
