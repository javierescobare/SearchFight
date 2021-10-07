using System;
namespace Searchfight.Services.Exceptions
{
    /// <summary>
    /// Exception raised from a search service.
    /// </summary>
    public class SearchServiceException : Exception
    {
        /// <summary>
        /// Engine name where the exception was raised.
        /// </summary>
        public string EngineName { get; private set; }

        /// <summary>
        /// Search service exception.
        /// </summary>
        /// <param name="engineName">Engine name where the exception was raised</param>
        /// <param name="message">Error message</param>
        /// <param name="innerException">Inner exception</param>
        public SearchServiceException(
            string engineName,
            string message,
            Exception innerException = null) : base(message, innerException)
        {
            this.EngineName = engineName;
        }
    }
}
