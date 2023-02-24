using System;
using Application;
using GrosvenorDeveloperPracticum;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrosvenorInHousePracticum
{
    class Program
    {
        static void Main(string[] args)
        {
            using IHost host = new IoC().BuildHost(args);

            Run(host.Services);

            host.RunAsync().Wait();
        }

        static void Run(IServiceProvider services)
        {

            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            IServer waiterService = serviceProvider.GetRequiredService<IServer>();

            while (true)
            {
                Console.WriteLine("Please, type your order." +
                                  "\nEach Dish Type is optional" +
                                  "\nYou must enter a comma delimited list of Dish Types with at least one selection" +
                                  "\nHere's a sample of order: morning, 1, 2, 3.");
                var unparsedOrder = Console.ReadLine();
                var output = waiterService.TakeOrder(unparsedOrder);
                Console.WriteLine(output + "\n");
            }
        }
    }
}
