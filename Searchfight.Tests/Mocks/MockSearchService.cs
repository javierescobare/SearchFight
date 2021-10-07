using System;
using System.Threading.Tasks;
using Searchfight.Services.Abstractions;
using Searchfight.Services.Models;

namespace Searchfight.Tests.Mocks
{
    public class MockSearchService : ISearchService
    {
        public string EngineName => "Mock";

        public Task<SearchCountResult> GetResultsCount(string keyword)
        {
            return Task.FromResult(
                new SearchCountResult
                {
                    Engine = EngineName,
                    ResultsCount = 10,
                    Query = keyword
                });
        }
    }
}
