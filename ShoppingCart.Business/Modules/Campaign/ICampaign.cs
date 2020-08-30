using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public interface ICampaign
    {
        /// <summary>
        /// which category the campaign is on
        /// </summary>
        ICategory Category { get; set; }

        /// <summary>
        /// Discount (represents rate or amount)
        /// </summary>
        double Discount { get; set; }

        /// <summary>
        /// min item count to apply the discount
        /// </summary>
        int ItemCount { get; set; }

        /// <summary>
        /// discount type
        /// </summary>
        DiscountType DiscountType { get; set; }

        /// <summary>
        /// gets the discount that will be applied
        /// </summary>
        /// <param name="amount">the amount to be discounted</param>
        /// <returns></returns>
        double GetDiscount(double amount);
    }
}
