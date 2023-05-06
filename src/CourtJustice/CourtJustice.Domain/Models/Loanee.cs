using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("loanee")]
    public class Loanee : BaseEntity
    {
        [Key]
        [Display(Name = "ID-CUST")]
        public string CusId { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ชื่อ-สกุล")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ที่อยู่ตาม ทร.")]
        public string Address { get; set; } = string.Empty;
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual AddressSet? AddressSet { set; get; }

        [Display(Name = "ที่อยู่ที่ติดต่อได้(1)")]
        public string? Address1 { get; set; } = string.Empty;
        public int Address1Id { get; set; } = 0;
        [ForeignKey(nameof(Address1Id))]
        public virtual AddressSet? AddressSet1 { set; get; }

        [Display(Name = "ที่อยู่ที่ติดต่อได้(2)")]
        public string? Address2 { get; set; } = string.Empty;
        public int Address2Id { get; set; } = 0;
        [ForeignKey(nameof(Address2Id))]
        public virtual AddressSet? AddressSet2 { set; get; }

        public int OccupationId { get; set; } = 0;
        [ForeignKey(nameof(OccupationId))]
        public virtual Occupation Occupation { set; get; } = new Occupation();
    }
}
