using System;
using System.Threading.Tasks;
using Infor.CPQ.ConfiguratorLoadTestFixtures;
using Zoxive.HttpLoadTesting.Framework.Core;
using Zoxive.HttpLoadTesting.Framework.Http;

namespace ReservationPerformanceTests.ReservationPerformanceTests
{
    internal class RunWoodDoor : ILoadTest
    {
        public string _tenant { get; }
        public RunWoodDoor(string tenant)
        {
            _tenant = tenant;
        }
        public string Name => nameof(RunWoodDoor);

        public async Task Execute(IUserLoadTestHttpClient loadLoadTestHttpClient)
        {
            var test = new ConfigurationV2(loadLoadTestHttpClient, _tenant, "AMC", "Wood Door v1")
                .WithIntegrationParameter("CurrencyCode", "", "string")
                .WithIntegrationParameter("ExchangeRate", "1", "number");

            try
            {
                await test.StartAsync();
                await test.ConfigureWithRandomOptionAsync("Select your wood species:", new string[] { }, "Species");
                await test.ConfigureWithRandomOptionAsync("Stain Color:", new string[] { }, "Stain");
                await test.Continue();
                await test.ConfigureAsync("Door Style:", "AA", "Set Style");
                await test.ConfigureAsync("Width:", "28", "Width");
                await test.ConfigureAsync("Height:", "84", "Height");
                await test.ConfigureAsync("Hinges:", "True", "Hinges");
                await test.Finalize();
                Console.WriteLine("Success");
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"FAILED {e.Message}");
                await test.Cancel();
                //throw e; 
            }

        }

        public Task Initialize(ILoadTestHttpClient loadLoadTestHttpClient)
        {
            return Task.CompletedTask;
        }
    }
}