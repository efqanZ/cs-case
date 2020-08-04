using CiSeCase.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

namespace CiSeCase.IntegrationTest
{
    public class ClientProvider : IDisposable
    {
        private readonly TestServer TestServer;
        public HttpClient HttpClient { get; set; }

        public ClientProvider()
        {
            var baseDirectory = Directory.GetCurrentDirectory();


            TestServer = new TestServer(new WebHostBuilder().UseStartup<Startup>().UseEnvironment("Development"));

            HttpClient = TestServer.CreateClient();
        }

        public void Dispose()
        {
            TestServer?.Dispose();
            HttpClient?.Dispose();
        }
    }
}