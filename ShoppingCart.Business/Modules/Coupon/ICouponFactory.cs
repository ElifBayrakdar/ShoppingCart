using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    /// <summary>
    /// this interface is for producing a coupon accourding to the discountType 
    /// </summary>
    public interface ICouponFactory
    {
        ICoupon ProduceCoupon(double minPurchaseAmount, double discount, DiscountType discountType);
    }
}
