using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("asset_salary")]
    public class AssetSalary
    {
        [Key]
        public int AssetId { get; set; }
        public string Company { get; set; } = string.Empty;
        public string Address { get; set; }=string.Empty;
        public decimal Salary { get; set; } = 0;
        public DateOnly SalaryDate { get; set; }

        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual AddressSet? AddressSet { set; get; }
    }
}
