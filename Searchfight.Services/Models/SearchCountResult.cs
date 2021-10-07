namespace Searchfight.Services.Models
{
    /// <summary>
    /// Result from search
    /// </summary>
    public class SearchCountResult
    {
        /// <summary>
        /// Searched query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Engine name
        /// </summary>
        public string Engine { get; set; }

        /// <summary>
        /// Estimated results count
        /// </summary>
        public long ResultsCount { get; set; }
    }

}
