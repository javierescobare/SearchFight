using System;
using Newtonsoft.Json;

namespace Searchfight.Services.Models.Google
{
    public class GoogleResponse
    {
        [JsonProperty("searchInformation")]
        public SearchInformation SearchInformation { get; set; }
    }
}
