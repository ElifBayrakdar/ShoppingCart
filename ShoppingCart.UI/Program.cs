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

            Category electronicCategory = new Category("Electronic");
            Product samsung = new Product("Samsung", 200, electronicCategory);
            Product nokia = new Product("Nokia", 100, electronicCategory);

            CampaignFactory campaignFactory = new CampaignFactory();
            AmountCampaign campaignFood = campaignFactory.ProduceCampaign(category, 25, 5, DiscountType.Amount) as AmountCampaign;
            AmountCampaign campaignElectronic = campaignFactory.ProduceCampaign(electronicCategory, 50, 1, DiscountType.Amount) as AmountCampaign;

            DeliveryCostCalculator calculator = new DeliveryCostCalculator(1, 2, 2.99d);
            Cart shoppingCart = new Cart(calculator);
            shoppingCart.AddItem(banana, 8);
            shoppingCart.AddItem(apple, 5);
            shoppingCart.AddItem(samsung, 1);
            shoppingCart.AddItem(nokia, 2);
            shoppingCart.ApplyDiscounts(campaignFood, campaignElectronic);

            shoppingCart.Print();
        }
    }
}
