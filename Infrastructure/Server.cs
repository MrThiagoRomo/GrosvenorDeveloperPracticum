using Application;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class Server : IServer
    {
        private readonly IDishManager _dishManager;

        public Server(IDishManager dishManager)
        {
            _dishManager = dishManager;
        }

        public string TakeOrder(string unparsedOrder)
        {
            if (string.IsNullOrEmpty(unparsedOrder))
            {
                return "error";
            }

            var treatedOrder = unparsedOrder.Trim().ToLower().ToLowerInvariant();

            try
            {
                Order order = ParseOrder(treatedOrder);
                string timeOfDay = GetTimeOfDay(treatedOrder);
                List<Dish> dishes = _dishManager.GetDishes(order, timeOfDay);
                string returnValue = FormatOutput(dishes);
                return returnValue;
            }
            catch (ApplicationException)
            {
                return "error";
            }
        }


        private Order ParseOrder(string unparsedOrder)
        {
            if (string.IsNullOrEmpty(unparsedOrder))
            {
                throw new ArgumentException("Order cannot be null or empty");
            }

            var returnValue = new Order
            {
                Dishes = new List<int>()
            };

            var orderItems = unparsedOrder.Split(',');
            var filteredOrderItems = orderItems.Where((item, index) => index != 0).ToArray();
            Array.Sort(filteredOrderItems);
            foreach (var orderItem in filteredOrderItems)
            {
                if (int.TryParse(orderItem, out int parsedOrder))
                {
                    returnValue.Dishes.Add(parsedOrder);
                }
                else
                {
                    throw new ApplicationException("Order needs to be comma separated list of numbers");
                }
            }
            return returnValue;
        }

        private string GetTimeOfDay(string unparsedOrder)
        {
            if (string.IsNullOrEmpty(unparsedOrder))
            {
                throw new ArgumentException("Order cannot be null or empty");
            }

            var orderItems = unparsedOrder.Split(',');
            var timeOfDay = orderItems[0].Trim();
            if (timeOfDay != "morning" && timeOfDay != "evening")
            {
                throw new ApplicationException("Time of day not recognized");
            }
            return timeOfDay;
        }

        private string FormatOutput(List<Dish> dishes)
        {
            var returnValue = "";

            foreach (var dish in dishes)
            {
                returnValue = returnValue + string.Format(",{0}{1}", dish.DishName, GetMultiple(dish.Count));
            }

            if (returnValue.StartsWith(","))
            {
                returnValue = returnValue.TrimStart(',');
            }

            return returnValue;
        }

        private object GetMultiple(int count)
        {
            if (count > 1)
            {
                return string.Format("(x{0})", count);
            }
            return "";
        }
    }
}
