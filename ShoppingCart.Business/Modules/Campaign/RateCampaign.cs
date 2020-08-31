using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public class RateCampaign : ICampaign
    {
        /// <summary>
        /// which category the campaign is on
        /// </summary>
        public ICategory Category { get; set; }

        /// <summary>
        /// Discount (based on rate)
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// min item count to apply the discount
        /// </summary>
        public int ItemCount { get; set; }

        /// <summary>
        /// discount type
        /// </summary>
        public DiscountType DiscountType { get; set; }

        public RateCampaign(ICategory category, double discount, int itemCount, DiscountType discountType)
        {
            this.Category = category;
            this.Discount = discount;
            this.ItemCount = itemCount;
            this.DiscountType = discountType;

            category.AddCampaign(this);
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
