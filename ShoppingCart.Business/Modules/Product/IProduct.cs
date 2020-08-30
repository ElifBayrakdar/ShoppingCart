using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public interface IProduct
    {
        /// <summary>
        /// title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// category of the product
        /// </summary>
        public ICategory Category { get; set; }
    }
}
