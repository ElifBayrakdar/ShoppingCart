using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShoppingCart.Business.Modules
{
    public class Cart : IShoppingCart
    {
        private readonly IDeliveryCostCalculator calculator;

        public Dictionary<Product, int> productItems = new Dictionary<Product, int>();

        public double TotalAmount { get; private set; } = 0;
        public double CampaignDiscount { get; private set; } = 0;
        public double CouponDiscount { get; private set; } = 0;

        public Cart(IDeliveryCostCalculator calculator)
        {
            this.calculator = calculator;
        }

        public void AddItem(Product product, int quantity)
        {
            if (productItems.ContainsKey(product))
            {
                productItems[product] += quantity;
            }
            else
            {
                productItems[product] = quantity;
            }

            TotalAmount += product.Price * quantity;
        }

        public void ApplyDiscounts(params ICampaign[] campaigns)
        {
            double discount = 0;
            double maxDiscount = 0;
            double productPriceBelongsToCategory = 0;
            foreach (var campaign in campaigns)
            {
                var productCountBelongsCategory = productItems.Where(x => x.Key.Category == campaign.Category).Sum(y => y.Value);
                if (productCountBelongsCategory >= campaign.ItemCount)
                {
                    productPriceBelongsToCategory = productItems.Where(x => x.Key.Category == campaign.Category).Sum(y => y.Key.Price * y.Value);

                    //dicsount on that category products
                    discount = campaign.GetDiscount(productPriceBelongsToCategory);

                    if (maxDiscount < discount)
                    {
                        maxDiscount = discount;
                    }
                }
            }
            CampaignDiscount = maxDiscount;
        }


        public void ApplyCoupon(ICoupon coupon)
        {
            if (TotalAmount >= coupon.MinPurchaseAmount)
            {
                CouponDiscount = coupon.GetDiscount(TotalAmount - CampaignDiscount);
            }
        }

        public double GetTotalAmountAfterDiscounts()
        {
            return TotalAmount - CampaignDiscount - CouponDiscount;
        }

        public double GetCouponDiscount()
        {
            return CouponDiscount;
        }

        public double GetCampaignDiscount()
        {
            return CampaignDiscount;
        }

        public double GetDeliveryCost()
        {
            return calculator.CalculateFor(this);
        }

        public void Print()
        {
            List<Product> orderedProductItemsByCategory = productItems.Keys.OrderBy(x => x.Category.Title).ToList();

            Console.WriteLine("|{0,15}|{1,15}|{2,15}|{3,15}|", "Category Name", "Product Name", "Quantity", "Unit Price");

            foreach (var item in orderedProductItemsByCategory)
            {
                Console.WriteLine("|{0,15}|{1,15}|{2,15}|{3,15}|", item.Category.Title, item.Title, productItems[item], item.Price);
            }
            Console.WriteLine("Total Price:{0}\tTotal Discount:{1}", TotalAmount, CampaignDiscount + CouponDiscount);
            Console.WriteLine("Total Amount:{0}\nDelivery Cost:{1}", GetTotalAmountAfterDiscounts(), GetDeliveryCost());
        }
    }
}
