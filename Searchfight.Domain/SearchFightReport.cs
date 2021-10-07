using System.Collections.Generic;
using System.Linq;
using System.Text;
using Searchfight.Services.Models;

namespace Searchfight.Domain
{
    /// <summary>
    /// Search fight report
    /// </summary>
    public class SearchFightReport
    {
        private readonly List<SearchCountResult> _searchResults = new List<SearchCountResult>();

        /// <summary>
        /// Adds a result to be considered in the report.
        /// </summary>
        /// <param name="searchResult">Search result</param>
        public void Add(SearchCountResult searchResult)
        {
            _searchResults.Add(searchResult);
        }

        /// <summary>
        /// Gets a formatted report for the given fight results. 
        /// </summary>
        /// <returns>Formatted report</returns>
        public string GetFormattedReport()
        {
            var report = new StringBuilder();

            var termResultsGroups = GetResultsByTerm();
            foreach (var resultGroup in termResultsGroups)
            {
                report.Append($"{resultGroup.Key} => ");
                foreach (var result in resultGroup)
                {
                    report.Append($"{result.Engine}: {result.ResultsCount} ");
                }
                report.AppendLine();
            }

            var winners = GetWinnersByEngine();
            foreach (var winner in winners)
            {
                report.AppendLine(winner.ToString());
            }

            var totalWinner = GetTotalWinner();
            report.AppendLine($"Total winner: {totalWinner}");
            return report.ToString();
        }

        /// <summary>
        /// Gets the results grouped by term.
        /// </summary>
        /// <returns>Results grouped by term</returns>
        public IEnumerable<IGrouping<string, SearchCountResult>> GetResultsByTerm()
        {
            return _searchResults.GroupBy(result => result.Query);
        }

        /// <summary>
        /// Gets the list of winners by engine.
        /// </summary>
        /// <returns>List of winners. Empty if no winners.</returns>
        public List<Winner> GetWinnersByEngine()
        {
            var winners = _searchResults
                .GroupBy(
                    result => result.Engine,
                    result => result,
                    (engine, results) => new Winner
                    {
                        Engine = engine,
                        Term = results.OrderByDescending(r => r.ResultsCount).FirstOrDefault()?.Query
                    })
                .ToList();
            return winners;
        }

        /// <summary>
        /// Gets the total winner term.
        /// </summary>
        /// <returns>Term or null if there is no winner</returns>
        public string GetTotalWinner()
        {
            return _searchResults
                .GroupBy(
                    result => result.Query,
                    result => result,
                    (term, result) => new { Term = term, Total = result.Sum(r => r.ResultsCount) })
                .OrderByDescending(r => r.Total)
                .FirstOrDefault()?
                .Term;
        }
    }

}
