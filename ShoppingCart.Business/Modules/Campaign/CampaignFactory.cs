using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    /// <summary>
    /// this class produces a campaign accourding to the discountType 
    /// </summary>
    public class CampaignFactory : ICampaignFactory
    {
        //Factory method pattern
        public ICampaign ProduceCampaign(Category category, double discount, int itemCount, DiscountType discountType)
        {
            ICampaign campaign = null;
            switch (discountType)
            {
                case DiscountType.Amount:
                    campaign = new AmountCampaign(category, discount, itemCount, discountType);
                    break;
                case DiscountType.Rate:
                    campaign = new RateCampaign(category, discount, itemCount, discountType);
                    break;
            }
            return campaign;
        }
    }
}
