using Microsoft.AspNetCore.Http;

namespace CourtJustice.Domain.ViewModels
{
    public class AssetImageViewModel
    {
        public string AssetId { get; set; } = string.Empty;
        public string CusId { get; set; } = string.Empty;
        public IList<IFormFile> Files { get; set; }
    }
}
