using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application;
using Infrastructure;

namespace GrosvenorDeveloperPracticum
{
    public class IoC
    {
        public IHost BuildHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services
                        .AddScoped<IDishManager, DishManager>()
                        .AddScoped<IServer, Server>()
                )
                .Build();
        }
    }
}
