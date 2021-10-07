using System.Collections.Generic;
using Searchfight.Services;
using Searchfight.Services.Abstractions;

namespace Searchfight.Client
{
    public class SearchServiceFactory
    {
        private readonly ConfigurationManager _configurationManager;

        public SearchServiceFactory()
        {
            _configurationManager = new ConfigurationManager();
        }

        public List<ISearchService> CreateAll()
        {
            var bingApiKey = _configurationManager.GetSettingByKey("BingApiKey");
            var googleApiKey = _configurationManager.GetSettingByKey("GoogleApiKey");
            var googleCx = _configurationManager.GetSettingByKey("GoogleCx");

            return new List<ISearchService>
            {
                new BingSearchService(bingApiKey),
                new GoogleSearchService(googleApiKey, googleCx)
            };
        }
    }

}
