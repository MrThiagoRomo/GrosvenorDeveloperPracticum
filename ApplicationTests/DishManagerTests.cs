using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Infrastructure;

namespace ApplicationTests
{
    [TestFixture]
    public class DishManagerTests
    {
        private DishManager _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new DishManager();
        }

        [Test]
        public void EmptyListReturnsEmptyList()
        {
            var order = new Order();
            var timeOfDay = string.Empty;
            var actual = _sut.GetDishes(order, timeOfDay);
            Assert.AreEqual(0, actual.Count);
        }

        [Test]
        public void ShouldReturnListWith1ReturnsOneSteak()
        {
            var order = new Order
            {
                Dishes = new List<int>
                {
                    1
                }
            };
            var timeOfDay = "evening";
            var actual = _sut.GetDishes(order, timeOfDay);
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("steak", actual.First().DishName);
            Assert.AreEqual(1, actual.First().Count);
        }

        [Test]
        public void ShouldReturnListWithCompleteMorningOrder()
        {
            // Arrange
            var order = new Order { Dishes = new List<int> { 1, 2, 3 } };
            var timeOfDay = "morning";

            // Act
            var actual = _sut.GetDishes(order, timeOfDay);

            // Assert
            Assert.AreEqual("egg", actual[0].DishName);
            Assert.AreEqual("toast", actual[1].DishName);
            Assert.AreEqual("coffee", actual[2].DishName);
        }

        [Test]
        public void ShouldReturnListWithMultipleCoffees()
        {
            // Arrange
            var order = new Order { Dishes = new List<int> { 3, 3, 3 } };
            var timeOfDay = "morning";

            // Act
            var actual = _sut.GetDishes(order, timeOfDay);

            // Assert
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("coffee", actual[0].DishName);
            Assert.AreEqual(3, actual.First().Count);
        }

        [Test]
        public void ShouldFailThrowsExceptionCannotHaveMultipleToasts()
        {
            // Arrange
            var order = new Order { Dishes = new List<int> { 1, 2, 2, 3 } };
            var timeOfDay = "morning";

            // Act
            var actual = Assert.Throws<ApplicationException>(() => _sut.GetDishes(order, timeOfDay));

            // Assert
            Assert.AreEqual("Multiple toast(s) not allowed", actual.Message);

        }

        [Test]
        public void ShouldFailThrowsExceptionWorngTimeOfDay()
        {
            // Arrange
            var order = 2;
            var returnValue = new List<Dish>();
            var timeOfDay = "mornig";

            // Act
            var actual = Assert.Throws<ApplicationException>(() => _sut.AddOrderToList(order, returnValue, timeOfDay));

            // Assert
            Assert.AreEqual("Order with wrong time of the day.", actual.Message);
        }

        [Test]
        public void ShouldFailThrowsExceptionOrderDoesNotExists()
        {
            // Arrange
            var order = 4;
            var returnValue = new List<Dish>();
            var timeOfDay = "morning";

            // Act
            var actual = Assert.Throws<ApplicationException>(() => _sut.AddOrderToList(order, returnValue, timeOfDay));

            // Assert
            Assert.AreEqual("Order does not exist", actual.Message);
        }

        [Test]
        public void ShouldReturnListWithCompleteEveningOrder()
        {
            // Arrange
            var order = new Order { Dishes = new List<int> { 1, 2, 3, 4 } };

            // Act
            var actual = _sut.GetDishes(order, "evening");

            // Assert
            Assert.AreEqual("steak", actual[0].DishName);
            Assert.AreEqual("potato", actual[1].DishName);
            Assert.AreEqual("wine", actual[2].DishName);
            Assert.AreEqual("cake", actual[3].DishName);
        }

    }
}
