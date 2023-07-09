using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("remain_task")]
    public class RemainTask
    {
        [Key]
        public int TaskId { get; set; }
        [Display(Name = "วันที่ทำรายการ")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime  TaskDatetime { get; set; }
        [Display(Name = "รายละเอียด")]
        public string TaskDetail { get; set; }
        [Display(Name = "พนักงาน")]
        public string AssignTo { get; set; }
        [Display(Name = "หัวหน้า")]
        public string AssignFrom { get; set; }
    }
}
