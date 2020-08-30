using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public class RateCoupon : ICoupon
    {
        public double MinPurchaseAmount { get; set; }
        public double Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public RateCoupon(double minPurchaseAmount, double discount, DiscountType discountType)
        {
            this.MinPurchaseAmount = minPurchaseAmount;
            this.Discount = discount;
            this.DiscountType = discountType;
        }

        public double GetDiscount(double amount)
        {
            return amount * Discount / 100;
        }
    }
}
