using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class DishManager : IDishManager
    {
        /// <summary>
        /// Takes an Order object, sorts the orders and builds a list of dishes to be returned. 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="timeOfDay"></param>
        /// <returns></returns>
        public List<Dish> GetDishes(Order order, string timeOfDay)
        {
            var returnValue = new List<Dish>();
            order.Dishes.Sort();
            foreach (var dishType in order.Dishes)
            {
                AddOrderToList(dishType, returnValue, timeOfDay);
            }
            return returnValue;
        }

        /// <summary>
        /// Takes an int, representing an order type, tries to find it in the list.
        /// If the dish type does not exist, add it and set count to 1
        /// If the type exists, check if multiples are allowed and increment that instances count by one
        /// else throw error
        /// </summary>
        /// <param name="order">int, represents a dishtype</param>
        /// <param name="returnValue">a list of dishes, - get appended to or changed </param>
        /// <param name="timeOfDay">int, represents a dishtype</param>
        public void AddOrderToList(int order, List<Dish> returnValue, string timeOfDay)
        {
            if (timeOfDay != "morning" && timeOfDay != "evening")
            {
                throw new ApplicationException("Order with wrong time of the day.");
            }

            var orderName = GetOrderName(order, timeOfDay);
            var existingOrder = returnValue.SingleOrDefault(x => x.DishName == orderName);
            if (existingOrder == null)
            {
                returnValue.Add(new Dish
                {
                    DishName = orderName,
                    Count = 1
                });
            } else if (IsMultipleAllowed(order, timeOfDay))
            {
                existingOrder.Count++;
            }
            else
            {
                throw new ApplicationException(string.Format("Multiple {0}(s) not allowed", orderName));
            }
        }

        private string GetOrderName(int order, string timeOfDay)
        {

            if (timeOfDay == "morning")
            {
                switch (order)
                {
                    case 1:
                        return "egg";
                    case 2:
                        return "toast";
                    case 3:
                        return "coffee";
                    default:
                        throw new ApplicationException("Order does not exist");

                }
            }
            if (timeOfDay == "evening")
            {
                switch (order)
                {
                    case 1:
                        return "steak";
                    case 2:
                        return "potato";
                    case 3:
                        return "wine";
                    case 4:
                        return "cake";
                    default:
                        throw new ApplicationException("Order does not exist");

                }
            }
            return null;
        }

        private bool IsMultipleAllowed(int order, string timeOfDay)
        {
            switch (order)
            {
                case 2:
                    if (timeOfDay == "evening")
                    {
                        return true;
                    }
                    return false;
                case 3:
                    if (timeOfDay == "morning")
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;

            }
        }
    }
}