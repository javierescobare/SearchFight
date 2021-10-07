using System;
using System.Threading.Tasks;
using Searchfight.Domain;

namespace Searchfight.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var factory = new SearchServiceFactory();
                var searchServices = factory.CreateAll();
                var fight = new SearchFight();
                fight.AddServices(searchServices);
                var fightResult = await fight.Execute(args);
                Console.WriteLine(fightResult.GetFormattedReport());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error ocurred.");
                Console.WriteLine(ex.Message);
            }
        }
    }

}
