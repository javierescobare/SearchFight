using Searchfight.Domain;
using Searchfight.Services.Models;
using Xunit;

namespace Searchfight.Tests
{
    public class SearchFightReportTests
    {
        private const string NetLanguage = ".NET";
        private const string JavaLanguage = "Java";

        private const string BingEngine = "Bing";
        private const string GoogleEngine = "Google";

        [Fact]
        public void GetTotalWinner_WithSameEngine_ShouldReturnTopCount()
        {
            var fightResult = new SearchFightReport();
            fightResult.Add(new SearchCountResult { Engine = BingEngine, ResultsCount = 30, Query = NetLanguage });
            fightResult.Add(new SearchCountResult { Engine = BingEngine, ResultsCount = 20, Query = JavaLanguage });

            var winner = fightResult.GetTotalWinner();

            Assert.Equal(NetLanguage, winner);
        }

        [Fact]
        public void GetTotalWinner_WithDifferentEngines_ShouldReturnTopCountSum()
        {
            var fightResult = new SearchFightReport();
            fightResult.Add(new SearchCountResult { Engine = BingEngine, ResultsCount = 30, Query = NetLanguage });
            fightResult.Add(new SearchCountResult { Engine = BingEngine, ResultsCount = 20, Query = JavaLanguage });
            fightResult.Add(new SearchCountResult { Engine = GoogleEngine, ResultsCount = 30, Query = NetLanguage });
            fightResult.Add(new SearchCountResult { Engine = GoogleEngine, ResultsCount = 80, Query = JavaLanguage });

            var winner = fightResult.GetTotalWinner();

            Assert.Equal(JavaLanguage, winner);
        }

        [Fact]
        public void GetTotalWinner_WithNoResults_ShouldReturnNull()
        {
            var fightResult = new SearchFightReport();
            var winner = fightResult.GetTotalWinner();

            Assert.Null(winner);
        }

        [Fact]
        public void GetWinners_WithManyEngines_ShouldReturnAsManyWinnersAsEngines()
        {
            var fightResult = new SearchFightReport();
            fightResult.Add(new SearchCountResult { Engine = BingEngine, ResultsCount = 30, Query = NetLanguage });
            fightResult.Add(new SearchCountResult { Engine = BingEngine, ResultsCount = 20, Query = JavaLanguage });
            fightResult.Add(new SearchCountResult { Engine = GoogleEngine, ResultsCount = 30, Query = NetLanguage });
            fightResult.Add(new SearchCountResult { Engine = GoogleEngine, ResultsCount = 80, Query = JavaLanguage });

            var winners = fightResult.GetWinnersByEngine();

            Assert.Equal(2, winners.Count);
        }

        [Fact]
        public void GetWinners_WithManyEngines_ShouldReturnTopCountPerEngine()
        {
            var fightResult = new SearchFightReport();
            fightResult.Add(new SearchCountResult { Engine = BingEngine, ResultsCount = 30, Query = NetLanguage });
            fightResult.Add(new SearchCountResult { Engine = BingEngine, ResultsCount = 20, Query = JavaLanguage });
            fightResult.Add(new SearchCountResult { Engine = GoogleEngine, ResultsCount = 30, Query = NetLanguage });
            fightResult.Add(new SearchCountResult { Engine = GoogleEngine, ResultsCount = 80, Query = JavaLanguage });

            var winners = fightResult.GetWinnersByEngine();

            var bingWinner = winners.Find(w => w.Engine == BingEngine);
            Assert.Equal(bingWinner.Term, NetLanguage);

            var googleWinner = winners.Find(w => w.Engine == GoogleEngine);
            Assert.Equal(googleWinner.Term, JavaLanguage);
        }

        [Fact]
        public void GetWinners_WithNoResults_ShouldReturnEmptyList()
        {
            var fightResult = new SearchFightReport();
            var winners = fightResult.GetWinnersByEngine();

            Assert.NotNull(winners);
            Assert.Empty(winners);
        }
    }
}
