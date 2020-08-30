using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public class Category : ICategory
    {
        /// <summary>
        /// parent-child hierarchy
        /// </summary>
        public ICategory ParentCategory { get; set; }  //Composite Pattern

        /// <summary>
        /// title
        /// </summary>
        public string Title { get; set; }

        public Category(string title)
        {
            this.Title = title;
        }
        public Category(string title, ICategory parentCategory)
        {
            this.Title = title;
            this.ParentCategory = parentCategory;
        }


        //Observer Pattern
        public readonly List<Product> products = new List<Product>();
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        //Observer Pattern
        public readonly List<ICampaign> campaigns = new List<ICampaign>();
        public void AddCampaign(ICampaign campaign)
        {
            campaigns.Add(campaign);
        }
    }
}
