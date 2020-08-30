using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    /// <summary>
    /// Observer of the category for new added campaigns
    /// </summary>
    public interface ICampaignObserver
    {
        void AddCampaign(ICampaign campaign);
    }
}
