using System;
using System.Threading.Tasks;
using Searchfight.Domain;
using Searchfight.Domain.Exceptions;
using Searchfight.Tests.Mocks;
using Xunit;

namespace Searchfight.Tests
{
    public class SearchFightTests
    {
        [Fact]
        public async Task Execute_WithNullArgs_ShouldThrowArgumentException()
        {
            var searchFight = new SearchFight();
            await Assert.ThrowsAsync<ArgumentException>(() => searchFight.Execute(null));
        }

        [Fact]
        public async Task Execute_WithNoArgs_ShouldThrowArgumentException()
        {
            var searchFight = new SearchFight();
            var terms = new string[0];
            await Assert.ThrowsAsync<ArgumentException>(() => searchFight.Execute(terms));
        }

        [Fact]
        public async Task Execute_With1Arg_ShouldThrowArgumentException()
        {
            var searchFight = new SearchFight();
            var terms = new[] { "java" };
            await Assert.ThrowsAsync<ArgumentException>(() => searchFight.Execute(terms));
        }

        [Fact]
        public async Task Execute_WithNoServices_ShouldThrowPreconditionException()
        {
            var searchFight = new SearchFight();
            var terms = new[] { "java", ".net" };
            await Assert.ThrowsAsync<SearchFightPreconditionException>(() => searchFight.Execute(terms));
        }

        [Fact]
        public async Task Execute_WithTwoServicesTwoArgs_ShouldNotThrowPreconditionException()
        {
            var searchFight = new SearchFight();
            var mock1 = new MockSearchService();
            var mock2 = new MockSearchService();
            searchFight.AddServices(new[] { mock1, mock2 });
            var terms = new[] { "java", ".net" };
            await searchFight.Execute(terms);
        }
    }
}
