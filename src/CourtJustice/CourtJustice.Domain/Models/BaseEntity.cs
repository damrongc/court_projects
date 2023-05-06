using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.Models
{
    public class BaseEntity
    {
        [Display(Name = "สถานะ")]
        public bool IsActive { get; set; } =true;
        [Display(Name = "ผู้บันทึก")]
        public string UserCreated { get; set; } =string.Empty;

        [Display(Name = "วันเวลาที่บันทึก")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Display(Name = "ผู้แก้ไข")]
        public string? UserUpdated { get; set; }
        [Display(Name = "วันเวลาที่แก้ไข")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime? UpdatedDateTime { get; set; }

    }
}
