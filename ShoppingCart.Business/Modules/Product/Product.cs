using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public class Product : IProduct
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public ICategory Category { get; set; }


        public Product(string title, double price, ICategory category)
        {
            this.Title = title;
            this.Price = price;
            this.Category = category;

            Category.AddProduct(this);
        }
    }
}
