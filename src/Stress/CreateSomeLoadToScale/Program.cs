using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CreateSomeLoadToScale
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var concurrentList = new List<Task>();

            for (var i = 0; i < 1000; i++)
            {
                concurrentList.Add(SendRequest());
            }

            Task.WaitAll(concurrentList.ToArray());

            for (var i = 0; i < 1000; i++)
            {
                concurrentList.Add(SendRequest());
            }

            Task.WaitAll(concurrentList.ToArray());

            for (var i = 0; i < 1000; i++)
            {
                concurrentList.Add(SendRequest());
            }

            Task.WaitAll(concurrentList.ToArray());

            Console.WriteLine("all done, press ke to close");

            Console.ReadLine();
        }

        private static async Task SendRequest()
        {
            var httpClient = new HttpClient();

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "http://barmanager.local/api/drinks");
                var response = await httpClient.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine("content received");
            }
            catch (Exception e)
            {
                Console.WriteLine("exception while receiving message");
            }
        }
    }
}
