using System;
namespace Searchfight.Domain.Exceptions
{
    /// <summary>
    /// Exception raised when there is a precondition that was not met.
    /// </summary>
    public class SearchFightPreconditionException : Exception
    {
        /// <summary>
        /// Search fight precondition exception.
        /// </summary>
        /// <param name="message">Error message</param>
        public SearchFightPreconditionException(string message) : base(message)
        {
        }
    }
}
