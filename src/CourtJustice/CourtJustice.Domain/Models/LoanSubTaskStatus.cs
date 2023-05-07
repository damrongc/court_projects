using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.Models
{
    [Table("loan_sub_task_status")]
    public class LoanSubTaskStatus 
    {
        [Key]
        [Display(Name = "รหัสกลุ่มงานย่อย")]
        public int LoanSubTaskStatusId { get; set; }
        [Display(Name = "กลุ่มงานย่อย")]
        public string LoanSubTaskStatusName { get; set; } = string.Empty;

        public int LoanTaskStatusId { get; set; }
    }
}
