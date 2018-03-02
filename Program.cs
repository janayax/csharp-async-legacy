using System;
using System.Threading.Tasks;

namespace LegacyAsync
{
    class Program
    {
        /*
        // Sync
        static void Main(string[] args)
        {
             Console.WriteLine("Begin");
            var managerApm = new ApmManager();
            managerApm.BeginApmStyleMining($"https://asynccoinfunction.azurewebsites.net/api/asynccoin/4");

            var managerEap = new EapManager();
            managerEap.EapStyleMiningAsync(4);
            
            Console.ReadLine();
        }*/

        static async Task Main(string[] args)
        {
            Console.WriteLine("start");
            var manager = new IntegrationManager();
            await manager.ExecuteAsync();
            Console.ReadLine();
        }
    }
}
