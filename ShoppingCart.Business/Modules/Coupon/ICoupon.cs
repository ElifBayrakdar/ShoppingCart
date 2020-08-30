using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public interface ICoupon
    {
        /// <summary>
        /// min purchase amount to apply coupon
        /// </summary>
        public double MinPurchaseAmount { get; set; }

        /// <summary>
        /// discount
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// discount type
        /// </summary>
        public DiscountType DiscountType { get; set; }

        /// <summary>
        /// gets the discount that will be applied
        /// </summary>
        /// <param name="amount">the amount to be discounted</param>
        /// <returns></returns>
        double GetDiscount(double amount);
    }
}
