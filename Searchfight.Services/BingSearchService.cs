using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Searchfight.Services.Abstractions;
using Searchfight.Services.Exceptions;
using Searchfight.Services.Models;
using Searchfight.Services.Models.Bing;

namespace Searchfight.Services
{
    public class BingSearchService : ISearchService
    {
        private const string BaseUri = "https://api.bing.microsoft.com/v7.0/search";
        private const string AuthorizationHeaderKey = "Ocp-Apim-Subscription-Key";

        public string EngineName => "Bing";
        private static readonly HttpClient _httpClient;

        static BingSearchService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUri),
            };
        }

        public BingSearchService(string bingApiKey)
        {
            if (string.IsNullOrWhiteSpace(bingApiKey))
            {
                throw new ArgumentNullException(nameof(bingApiKey));
            }

            _httpClient.DefaultRequestHeaders
                .Add(AuthorizationHeaderKey, bingApiKey);
        }

        public async Task<SearchCountResult> GetResultsCount(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            try
            {
                using var response = await _httpClient
                    .GetAsync($"?q={query}")
                    .ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    throw new SearchServiceException(
                        EngineName,
                        $"Error retrieving results for {query}");
                }

                var jsonResult = await response.Content.ReadAsStringAsync();
                var bingResponse = JsonConvert.DeserializeObject<BingResponse>(jsonResult);
                return new SearchCountResult {
                    Engine = EngineName,
                    Query = query,
                    ResultsCount = bingResponse.WebPages.TotalEstimatedMatches
                };
            }
            catch (SearchServiceException serviceEx)
            {
                Console.WriteLine($"Error in {serviceEx.EngineName} service: {serviceEx.Message}");
                throw;
            }
        }
    }
}
