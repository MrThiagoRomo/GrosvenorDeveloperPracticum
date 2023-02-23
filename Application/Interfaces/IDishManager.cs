using System.Collections.Generic;
using Domain;

namespace Application
{

    public interface IDishManager
    {
        /// <summary>
        /// Constructs a list of dishes, each dish with a name and a count
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        List<Dish> GetDishes(Order order, string timeOfDay);
    }
}