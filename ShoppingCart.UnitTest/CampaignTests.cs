using ShoppingCart.Business;
using ShoppingCart.Business.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShoppingCart.UnitTest
{
    public class CampaignTests
    {
        [Fact]
        public void GetDiscount_WhenCalledForAmountCampaign_ReturnsDiscount()
        {
            //Arrange
            Category category = new Category("Food");
            CampaignFactory factory = new CampaignFactory();
            ICampaign campaign = factory.ProduceCampaign(category, 20, 3, DiscountType.Amount);

            //Act
            double discount = campaign.GetDiscount(120);

            //Assert
            Assert.Equal(campaign.Discount, discount);
        }

        [Fact]
        public void GetDiscount_WhenCalledForAmountCampaign_ReturnsPercentageOfDiscount()
        {
            //Arrange
            double discountRate = 10;
            double givenAmount = 200;
            double expectedDiscount = 20;
            Category category = new Category("Food");
            CampaignFactory factory = new CampaignFactory();
            ICampaign campaign = factory.ProduceCampaign(category, discountRate, 3, DiscountType.Rate);

            //Act
            double discount = campaign.GetDiscount(givenAmount);

            //Assert
            Assert.Equal(expectedDiscount, discount);
        }
    }
}
