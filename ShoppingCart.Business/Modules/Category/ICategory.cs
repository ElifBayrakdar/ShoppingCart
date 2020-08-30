using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public interface ICategory : IProductObserver, ICampaignObserver
    {
        /// <summary>
        /// title of the category
        /// </summary>
        public string Title { get; set; }
    }
}
