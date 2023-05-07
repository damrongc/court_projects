using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtJustice.Domain.Models
{
    [Table("loan_task_status")]
    public class LoanTaskStatus
    {
        [Key]
        [Display(Name = "รหัสกลุ่มงาน")]
        public int LoanTaskStatusId { get; set; }
        [Display(Name = "กลุ่มงาน")]
        public string LoanTaskStatusName { get; set; } = string.Empty; 
    }
}
