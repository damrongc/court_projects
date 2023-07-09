using Newtonsoft.Json;

namespace CourtJustice.Domain.ViewModels
{
    public class SummaryChartViewModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("subTitle")]
        public string SubTitle { get; set; }
        public string[] Categories { get; set; }
        public int[] CountValues { get; set; }
        public decimal[] TotalValues { get; set; }
    }
}
