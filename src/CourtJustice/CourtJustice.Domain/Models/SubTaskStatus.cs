using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.Models
{
    [Table("sub_task_status")]
    public class SubTaskStatus : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสกลุ่มงานย่อย")]
        public int SubTaskStatusId { get; set; }
        [Display(Name = "กลุ่มงานย่อย")]
        public string SubTaskStatusName { get; set; } = string.Empty;

        public int TaskStatusId { get; set; }
    }
}
