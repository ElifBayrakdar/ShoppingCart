using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShoppingCart.Business.Modules
{
    public class DeliveryCostCalculator : IDeliveryCostCalculator
    {
        /// <summary>
        /// cost per delivery in the cart (per category)
        /// </summary>
        public double CostPerDelivery { get; set; }

        /// <summary>
        /// cost per product type in the cart
        /// </summary>
        public double CostPerProduct { get; set; }

        /// <summary>
        /// a fixed cost
        /// </summary>
        public double FixedCost { get; set; }
        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost)
        {
            this.CostPerDelivery = costPerDelivery;
            this.CostPerProduct = costPerProduct;
            this.FixedCost = fixedCost;
        }

        public double CalculateFor(Cart cart)
        {
            if(cart is null)
            {
                throw new InvalidOperationException("There is no cart!");
            }

            if (cart.productItems.Count == 0)
            {
                throw new InvalidOperationException("Shopping Cart is empty!");
            }

            int numberOfDelivery = cart.productItems.Keys.ToList().GroupBy(x => x.Category).Count();

            return (CostPerDelivery * numberOfDelivery) + (CostPerProduct * cart.productItems.Count) + FixedCost;
        }
    }
}
