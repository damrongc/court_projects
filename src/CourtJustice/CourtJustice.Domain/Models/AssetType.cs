using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Domain.Models
{
    [Table("asset_type")]
    public class AssetType
    {
        [Key]
        [Display(Name = "รหัสประเภทหลักประกัน")]
        public int AssetTypeId { get; set; }
        [Required]
        [Display(Name = "ประเภทหลักประกัน")]
        public string AssetTypeName { get; set; }=string.Empty;
    }
}
