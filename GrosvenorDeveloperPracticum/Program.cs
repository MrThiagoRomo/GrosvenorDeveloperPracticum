using System;
using Application;
using Infrastructure;

namespace GrosvenorInHousePracticum
{
    class Program
    {
        static void Main()
        {
            var server = new Server(new DishManager());
            while (true)
            {
                Console.WriteLine("Please, type your order." +
                    "\nEach Dish Type is optional" +
                    "\nYou must enter a comma delimited list of Dish Types with at least one selection" +
                    "\nHere's a sample of order: morning, 1, 2, 3.");
                var unparsedOrder = Console.ReadLine();
                var output = server.TakeOrder(unparsedOrder);
                Console.WriteLine(output);
            }
        }
    }
}
