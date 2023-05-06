using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CourtJustice.Domain.Models
{
    [Table("referencer")]
    public class Referencer
    {
        [Key]
        [Display(Name = "รหัสบุคคลอ้างอิง")]
        public string ReferencerCode { get; set; } = string.Empty;
        [Display(Name = "ชื่อ")]
        public string FirstName { get; set; } = string.Empty;
        [Display(Name = "นามสกุล")]
        public string LastName { get; set; } = string.Empty;
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่")]
        public string Address { get; set; } = string.Empty;
        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual AddressSet? AddressSet { set; get; }
    }
}
