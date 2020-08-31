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

        /// <summary>
        /// adds new product with quantity info
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddItem(Product product, int quantity)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

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

        /// <summary>
        /// applies max discount to cart for campaigns
        /// </summary>
        /// <param name="campaigns"></param>
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

        /// <summary>
        /// applies discount to cart for coupon
        /// </summary>
        /// <param name="coupon"></param>
        public void ApplyCoupon(ICoupon coupon)
        {
            if(coupon is null)
                throw new ArgumentNullException(nameof(coupon));

            if (TotalAmount >= coupon.MinPurchaseAmount)
            {
                CouponDiscount = coupon.GetDiscount(TotalAmount - CampaignDiscount);
            }
        }

        /// <summary>
        /// gets the total amount after all discounts applied
        /// </summary>
        /// <returns></returns>
        public double GetTotalAmountAfterDiscounts()
        {
            return TotalAmount - CampaignDiscount - CouponDiscount;
        }

        /// <summary>
        /// gets the discount of coupon
        /// </summary>
        /// <returns></returns>
        public double GetCouponDiscount()
        {
            return CouponDiscount;
        }

        /// <summary>
        /// gets the campaign discount which applied to cart
        /// </summary>
        /// <returns></returns>
        public double GetCampaignDiscount()
        {
            return CampaignDiscount;
        }

        /// <summary>
        /// gets delivery cost of cart
        /// </summary>
        /// <returns></returns>
        public double GetDeliveryCost()
        {
            return calculator.CalculateFor(this);
        }

        /// <summary>
        /// prints the billing details
        /// </summary>
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
