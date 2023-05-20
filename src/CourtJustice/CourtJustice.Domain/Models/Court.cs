using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("court")]
    public class Court : BaseEntity
    {
        [Key]
        public int CourtId { get; set; }
        public string CourtName { get; set; }

        [Display(Name = "ที่อยู่ศาล")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]
        public string AddressDetail { get; set; } = string.Empty;

    }
}
