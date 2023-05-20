using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("occupation")]
    public class Occupation : BaseEntity
    {
        [Key]
        [Display(Name = "รหัส")]
        public int OccupationId { get; set; }
        [Display(Name = "อาชีพ")]
        public string OccupationName { get; set; } =string.Empty;
    }
}
