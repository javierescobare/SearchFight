using Newtonsoft.Json;

namespace Searchfight.Services.Models.Bing
{
    public class WebPages
    {
        [JsonProperty("totalEstimatedMatches")]
        public long TotalEstimatedMatches { get; set; }
    }
}
