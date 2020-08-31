﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public class RateCoupon : ICoupon
    {
        /// <summary>
        /// min purchase amount to apply coupon
        /// </summary>
        public double MinPurchaseAmount { get; set; }

        /// <summary>
        /// discount (based on rate)
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// discount type
        /// </summary>
        public DiscountType DiscountType { get; set; }
        public RateCoupon(double minPurchaseAmount, double discount, DiscountType discountType)
        {
            this.MinPurchaseAmount = minPurchaseAmount;
            this.Discount = discount;
            this.DiscountType = discountType;
        }

        /// <summary>
        /// gets the discount that will be applied
        /// </summary>
        /// <param name="amount">the amount to be discounted</param>
        /// <returns></returns>
        public double GetDiscount(double amount)
        {
            return amount * Discount / 100;
        }
    }
}
