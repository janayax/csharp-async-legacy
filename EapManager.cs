using System;
using System.IO;
using System.Net;
using System.Threading;

namespace LegacyAsync
{
    /*  Microsoft created the Event-based Asynchronous Pattern (EAP) for the release of .NET Framework 2.0. 
    EAP relies on an instance MethodNameAsync method which returns void, accepts the same parameters as 
    a corresponding synchronous method, and initiates the asynchronous operation.

    You will notice that EAP has similar naming guidance as TAP, however EAP methods have a structure that differs from both APM and TAP methods. 
    EAP methods use one or more event handlers to perform asynchronous operations, rather than unifying on the Task class. The event handlers are 
    typically custom delegate types that utilize event argument types that are or that are derived from ProgressChangedEventArgs and AsyncCompletedEventArgs. */
    public class EapManager
    {
        private DateTime _startMiningDateTimeUtc;

        public void EapStyleMiningAsync(int requestedAmount)
        {
            _startMiningDateTimeUtc = DateTime.UtcNow;
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(EapMining_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri($"https://asynccoinfunction.azurewebsites.net/api/asynccoin/{requestedAmount}"));
        }

        private void EapMining_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new Exception(e.Error.Message);

            var miningResult = e.Result.ToString();
            double elapsedSeconds = (DateTime.UtcNow - _startMiningDateTimeUtc).TotalSeconds;
            Console.WriteLine(miningResult);
            Console.WriteLine($"Elapsed seconds: {elapsedSeconds}");
        }


    }
}
