using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    /// <summary>
    /// Observer of the category for new added products
    /// </summary>
    public interface IProductObserver
    {
        void AddProduct(Product product);
    }
}
