using System;
using System.Threading;
using System.Threading.Tasks;
using DomainInterfaces;
using Microsoft.Extensions.Hosting;
using Orleans;

namespace SiloClient
{
    public class ProductClientHostedService : IHostedService
    {
        private readonly IClusterClient clusterClient;

        public ProductClientHostedService(IClusterClient client, IApplicationLifetime lifetime) => this.clusterClient = client;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("=======================");
            // example of calling grains from the initialized clusterClient
            var product = clusterClient.GetGrain<IProduct>(0);
            var name = await product.GetName();
            Console.WriteLine("\n\n{0}\n\n", name);
            var price = await product.GetPrice();
            Console.WriteLine("\n\n{0}\n\n", price);
            Console.WriteLine("=======================");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
