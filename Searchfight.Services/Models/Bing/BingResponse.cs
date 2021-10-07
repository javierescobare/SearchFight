using System;
using Newtonsoft.Json;

namespace Searchfight.Services.Models.Bing
{
    public class BingResponse
    {
        [JsonProperty("webPages")]
        public WebPages WebPages { get; set; }
    }
}
