using System.Collections.Generic;
using Infor.CPQ.ConfiguratorLoadTestFixtures;
using ReservationPerformanceTests.ReservationPerformanceTests;
using Zoxive.HttpLoadTesting.Client;
using Zoxive.HttpLoadTesting.Framework.Core;
using Zoxive.HttpLoadTesting.Framework.Core.Schedules;
using Zoxive.HttpLoadTesting.Framework.Model;

namespace ReservationPerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify schedules. Add a few users run for a while and remove them. You can run any schedule in any order.
            // As long as you have active users!
            var schedule = new List<ISchedule>
            {    
                // Add Users over a period of time
                new AddUsers(totalUsers: 10, usersEvery: 1, seconds: 1),
                //new AddUsers(totalUsers: 30, usersEvery: 1, seconds: 15),
                // Run for a duration of time
                new Duration(15m),
                // Remove Users over a period of time
                new RemoveUsers(usersToRemove: 10, usersEvery:1, seconds: 1)
            };

            var tests = new List<ILoadTest>
            {
                new RunWoodDoor("CPQUSAX3_AX3")
            };

            var users = new List<IHttpUser>
            {
                UserFactory.CreateUser("https://cfgax3.cpq.awsdev.infor.com/", "key here", "secret here", "CPQUSAX3_AX3", tests),
            };

            var testExecution = new LoadTestExecution(users);
            
            WebClient.Run(testExecution, schedule, null, args);
        }  
    }
}
