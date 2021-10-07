using Newtonsoft.Json;

namespace Searchfight.Services.Models.Google
{
    public class SearchInformation
    {
        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }
    }
}
