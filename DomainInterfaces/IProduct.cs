using System;
using System.Threading.Tasks;
using Orleans;

namespace DomainInterfaces
{
    public interface IProduct : IGrainWithIntegerKey
    {
        Task<string> GetName();
        Task<decimal> GetPrice();
    }
}
