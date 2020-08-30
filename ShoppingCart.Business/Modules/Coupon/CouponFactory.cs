using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    /// <summary>
    /// this class is for producing a coupon accourding to the discountType 
    /// </summary>
    public class CouponFactory : ICouponFactory
    {
        //Factory method pattern
        public ICoupon ProduceCoupon(double minPurchaseAmount, double discount, DiscountType discountType)
        {
            ICoupon coupon = null;
            switch (discountType)
            {
                case DiscountType.Amount:
                    coupon = new AmountCoupon(minPurchaseAmount, discount, discountType);
                    break;
                case DiscountType.Rate:
                    coupon = new RateCoupon(minPurchaseAmount, discount, discountType);
                    break;
            }
            return coupon;
        }
    }
}
