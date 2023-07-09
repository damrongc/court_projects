using Newtonsoft.Json;

namespace CourtJustice.Domain.ViewModels
{
    public class ChartViewModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("subTitle")]
        public string SubTitle { get; set; }

        [JsonProperty("categories")]
        public string[] Categories { get; set; }

        [JsonProperty("series")]
        public Series[] Series { get; set; }
    }

    public class Series
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public decimal[] Data { get; set; }
        [JsonProperty("showInLegend")]
        public bool ShowInLegend { get; set; }
        [JsonProperty("visible")]
        public bool Visible { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
