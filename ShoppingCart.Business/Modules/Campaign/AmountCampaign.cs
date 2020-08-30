﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Modules
{
    public class AmountCampaign : ICampaign
    {
        public ICategory Category { get; set; }
        public double Discount { get; set; }
        public int ItemCount { get; set; }
        public DiscountType DiscountType { get; set; }

        public AmountCampaign(ICategory category, double discount, int itemCount, DiscountType discountType)
        {
            this.Category = category;
            this.Discount = discount;
            this.ItemCount = itemCount;
            this.DiscountType = discountType;

            category.AddCampaign(this);
        }

        public double GetDiscount(double amount)
        {
            return Discount;
        }
    }
}