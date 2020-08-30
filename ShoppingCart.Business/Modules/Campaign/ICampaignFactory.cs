using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    /// <summary>
    /// this interface is for producing a campaign accourding to the discountType 
    /// </summary>
    public interface ICampaignFactory
    {
        ICampaign ProduceCampaign(Category category, double discount, int itemCount, DiscountType discountType);
    }
}
