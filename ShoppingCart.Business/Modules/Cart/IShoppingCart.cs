using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public interface IShoppingCart
    {
        /// <summary>
        /// adds new product with quantity info
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        void AddItem(Product product, int quantity);

        /// <summary>
        /// applies discount to cart for campaigns
        /// </summary>
        /// <param name="campaigns"></param>
        void ApplyDiscounts(params ICampaign[] campaigns);

        /// <summary>
        /// applies discount to cart for coupon
        /// </summary>
        /// <param name="coupon"></param>
        void ApplyCoupon(ICoupon coupon);

        /// <summary>
        /// gets the total amount after all discounts applied
        /// </summary>
        /// <returns></returns>
        double GetTotalAmountAfterDiscounts();

        /// <summary>
        /// gets the discount of coupon
        /// </summary>
        /// <returns></returns>
        double GetCouponDiscount();

        /// <summary>
        /// gets the campaign discount which applied to cart
        /// </summary>
        /// <returns></returns>
        double GetCampaignDiscount();

        /// <summary>
        /// gets delivery cost of cart
        /// </summary>
        /// <returns></returns>
        double GetDeliveryCost();

        /// <summary>
        /// prints the billing details
        /// </summary>
        void Print();
    }
}
