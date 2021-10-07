using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Searchfight.Domain.Exceptions;
using Searchfight.Services.Abstractions;
using Searchfight.Services.Models;

namespace Searchfight.Domain
{
    /// <summary>
    /// Search fight organizer
    /// </summary>
    public class SearchFight
    {
        private readonly List<ISearchService> _searchServices = new List<ISearchService>();

        /// <summary>
        /// Adds search services to be used.
        /// </summary>
        /// <param name="searchServices"></param>
        public void AddServices(IList<ISearchService> searchServices)
        {
            _searchServices.AddRange(searchServices);
        }

        /// <summary>
        /// Executes the fight with the given terms.
        /// </summary>
        /// <param name="terms">Terms to be searched</param>
        /// <returns></returns>
        public async Task<SearchFightReport> Execute(IList<string> terms)
        {
            if (terms == null || terms.Count < 2)
            {
                throw new ArgumentException("You must enter at least 2 terms to search", nameof(terms));
            }
            if (_searchServices.Count < 2)
            {
                throw new SearchFightPreconditionException("You must set at least 2 search services to start a fight");
            }

            var searchTasks = new List<Task<SearchCountResult>>();
            foreach (var searchService in _searchServices)
            {
                foreach (var term in terms)
                {
                    searchTasks.Add(searchService.GetResultsCount(term));
                }
            }
            await Task.WhenAll(searchTasks);
            return MapResults(searchTasks);
        }

        private SearchFightReport MapResults(List<Task<SearchCountResult>> searchTasks)
        {
            var fightResult = new SearchFightReport();
            foreach (var task in searchTasks)
            {
                fightResult.Add(task.Result);
            }
            return fightResult;
        }
    }

}
