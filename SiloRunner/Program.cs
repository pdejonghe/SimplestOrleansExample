using System;
using System.Net;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace SiloRunner
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder                        
                        .UseLocalhostClustering()
                        //ClusterId is the name for the Orleans cluster must be the same for silo and client so they can talk to each other.
                        //ServiceId is the ID used for the application and it must not change across deployments
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "HelloWorldApp";
                        })
                        //This tells the silo where to listen.
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        //Adds the grain class and interface assembly as application parts to your orleans application.
                        //Make sure to reference NuGet package Microsoft.Orleans.CodeGenerator.MsBuild in both domain as interface projects ...
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(Product).Assembly).WithReferences());
                })
                .ConfigureServices(services =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}
