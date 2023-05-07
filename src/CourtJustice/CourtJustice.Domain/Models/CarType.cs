using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Domain.Models
{
    [Table("car_type")]
    public class CarType
    {
        [Key]
        [Display(Name = "รหัสประเภทรถ")]
        public int CarTypeCode { get; set; }
        [Required]
        [Display(Name = "ประเภทรถ")]
        public string CarTypeName { get; set; } = string.Empty;
    }
}
