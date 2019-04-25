using System;
using System.Threading.Tasks;
using DomainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Domain
{
    public class Product : Grain, IProduct
    {
        private readonly ILogger logger;
        public Product(ILogger<Product> logger) => this.logger = logger;
        public Task<string> GetName()
        {
            logger.LogInformation("Inside GetName() method");
            return Task.FromResult("Tooth paste");
        }

        public Task<decimal> GetPrice()
        {
            logger.LogInformation("Inside GetPrice() method");
            return Task.FromResult(12.5m);
        }
    }
}
