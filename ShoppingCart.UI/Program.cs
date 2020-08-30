using ShoppingCart.Business;
using ShoppingCart.Business.Modules;
using System;

namespace ShoppingCart.UI
{
    /// <summary>
    /// test for UI
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            Product apple = new Product("Apple", 15, category);

            CampaignFactory campaignFactory = new CampaignFactory();
            AmountCampaign campaign = campaignFactory.ProduceCampaign(category, 25, 5, DiscountType.Amount) as AmountCampaign;

            DeliveryCostCalculator calculator = new DeliveryCostCalculator(1, 2, 2.99d);
            Cart shoppingCart = new Cart(calculator);
            shoppingCart.AddItem(banana, 5);
            shoppingCart.AddItem(apple, 5);
            shoppingCart.ApplyDiscounts(campaign);

            shoppingCart.Print();
        }
    }
}
