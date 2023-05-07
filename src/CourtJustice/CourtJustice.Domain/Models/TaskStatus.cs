using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("task_status")]
    public class TaskStatus : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสกลุ่มงาน")]
        public int TaskStatusId { get; set; }
        [Display(Name = "กลุ่มงาน")]
        public string TaskStatusName { get; set; } = string.Empty; 
    }
}
