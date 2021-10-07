using System.Threading.Tasks;
using Searchfight.Services.Models;

namespace Searchfight.Services.Abstractions
{
    /// <summary>
    /// Search service.
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Engine name.
        /// </summary>
        public string EngineName { get; }

        /// <summary>
        /// Executes a search for the given query and returns the estimated
        /// number of results.
        /// </summary>
        /// <param name="query">Query to be searched.</param>
        /// <returns></returns>
        public Task<SearchCountResult> GetResultsCount(string query);
    }
}
