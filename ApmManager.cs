using System;
using System.IO;
using System.Net;
using System.Threading;

namespace LegacyAsync
{
    /* Microsoft developed the Asynchronous Programming Model as guidance for how leverage the .NET Framework's tools for writing asynchronous code. 
      The APM uses the IAsyncResult class as one of its central concepts. IAsyncResult enables a wide range of asynchronous code that is conceptually 
      similar to the things you can do with Task/TAP. In general, though, working with IAsyncResult is more complex than working with Task.

      To create asynchronous code with IAsyncResult, you need at least two methods 
        – A calling method and a callback method. Things can get more complex from there, 
        with other use cases such as polling, AsyncWaitHandles and AsyncCallback delegates. */
    public class ApmManager
    {
        private DateTime _startMiningDateTimeUtc;

        #region async request/response

        public void BeginApmStyleMining(string url)
        {
            _startMiningDateTimeUtc = DateTime.UtcNow;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            IAsyncResult result = request.BeginGetResponse(new AsyncCallback(EndApmStyleMining), request);
        }

        public void EndApmStyleMining(IAsyncResult result)
        {
            HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
            string miningResult;
            using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
            {
                miningResult = httpWebStreamReader.ReadToEnd();
            }
            double elapsedSeconds = (DateTime.UtcNow - _startMiningDateTimeUtc).TotalSeconds;
            Console.WriteLine(miningResult);
            Console.WriteLine($"Elapsed seconds: {elapsedSeconds}");
        }

        #endregion

    }
}
