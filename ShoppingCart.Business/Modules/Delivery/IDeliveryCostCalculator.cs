using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public interface IDeliveryCostCalculator
    {
        /// <summary>
        /// calculates the delivery cost
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        double CalculateFor(Cart cart);
    }
}
