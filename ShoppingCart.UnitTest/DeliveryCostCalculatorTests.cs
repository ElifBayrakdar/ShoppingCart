using ShoppingCart.Business;
using ShoppingCart.Business.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShoppingCart.UnitTest
{
    public class DeliveryCostCalculatorTests
    {
        [Fact]
        public void CalculateFor_WhenCalledWithEmptyCart_ThrowsException()
        {
            //Arrange
            DeliveryCostCalculator calculator = new DeliveryCostCalculator(1, 2, 2.99d);
            Cart shoppingCart = new Cart(calculator);

            //Act && Assert
            var exception = Assert.Throws<InvalidOperationException>(() => calculator.CalculateFor(shoppingCart));
            Assert.Equal("Shopping Cart is empty!", exception.Message);
        }

        [Fact]
        public void CalculateFor_WhenCalledWithNullParameter_ThrowsException()
        {
            //Arrange
            DeliveryCostCalculator calculator = new DeliveryCostCalculator(1, 2, 2.99d);

            //Act && Assert
            var exception = Assert.Throws<InvalidOperationException>(() => calculator.CalculateFor(null));
            Assert.Equal("There is no cart!", exception.Message);
        }

        [Fact]
        public void CalculateFor_WhenCalledWithShoppingCart_CalculatesTheDeliveryCostForCart()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            DeliveryCostCalculator calculator = new DeliveryCostCalculator(1, 2, 2.99d);
            Cart shoppingCart = new Cart(calculator);
            shoppingCart.AddItem(banana, 5);

            //Act
            double deliveryCost = calculator.CalculateFor(shoppingCart);

            //Assert
            Assert.Equal(5.99d, deliveryCost);
        }
    }
}
