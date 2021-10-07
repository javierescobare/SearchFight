namespace Searchfight.Domain
{
    /// <summary>
    /// Fight winner
    /// </summary>
    public class Winner
    {
        /// <summary>
        /// Engine
        /// </summary>
        public string Engine { get; set; }

        /// <summary>
        /// Term that was searched
        /// </summary>
        public string Term { get; set; }

        public override string ToString()
        {
            return $"{Engine} winner: {Term}";
        }
    }

}
