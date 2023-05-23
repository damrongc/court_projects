using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CourtJustice.Domain.Models
{
    [Table("asset_image")]
    public class AssetImage
    {
        [Key]
        public int ImageId { get; set; }
        public string AssetId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;


    }
}
