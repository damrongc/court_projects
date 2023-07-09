using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.Models
{
    [Table("month_model")]
    public class MonthModel
    {
        [Key]
        public int MonthId { get; set; }

        [MaxLength(45)]
        public string MonthName { get; set; }
    }
}
