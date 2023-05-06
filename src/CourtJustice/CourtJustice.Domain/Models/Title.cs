using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Domain.Models
{
    [Table("title")]
    public class Title :BaseEntity
    {
        [Key]
        [Display(Name = "รหัสคำนำหน้า")]
        public  string TitleCode { get; set; } =string.Empty;
        [Required]
        [Display(Name = "คำนำหน้า")]
        public string TitleName { get; set; } = string.Empty;
    }
}
