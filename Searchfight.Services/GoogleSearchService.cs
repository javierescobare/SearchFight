using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Searchfight.Services.Abstractions;
using Searchfight.Services.Exceptions;
using Searchfight.Services.Models;
using Searchfight.Services.Models.Google;

namespace Searchfight.Services
{
    public class GoogleSearchService : ISearchService
    {
        private const string BaseUri = "https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}";

        public string EngineName => "Google";
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiKey;
        private readonly string _cx;

        public GoogleSearchService(string googleApiKey, string googleCx)
        {
            if (string.IsNullOrWhiteSpace(googleApiKey))
            {
                throw new ArgumentNullException(nameof(googleApiKey));
            }
            if (string.IsNullOrWhiteSpace(googleCx))
            {
                throw new ArgumentNullException(nameof(googleCx));
            }

            _apiKey = googleApiKey;
            _cx = googleCx;
        }

        public async Task<SearchCountResult> GetResultsCount(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            try
            {
                string googleUri = string.Format(BaseUri, _apiKey, _cx, query);
                using var response = await _httpClient.GetAsync(googleUri).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    throw new SearchServiceException(
                        EngineName,
                        $"Error retrieving results for '{query}'");
                }

                var jsonResult = await response.Content.ReadAsStringAsync();
                var googleResponse = JsonConvert.DeserializeObject<GoogleResponse>(jsonResult);
                return new SearchCountResult
                {
                    Engine = EngineName,
                    Query = query,
                    ResultsCount = googleResponse.SearchInformation.TotalResults
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
