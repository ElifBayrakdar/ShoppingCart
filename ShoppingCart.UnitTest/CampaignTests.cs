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
        public void ProduceCampaign_WhenCalledForAmountCampaign_ReturnsAmountCampaignObject()
        {
            //Arrange
            Category category = new Category("Food");
            CampaignFactory factory = new CampaignFactory();

            //Act
            ICampaign coupon = factory.ProduceCampaign(category, 50, 10, DiscountType.Amount);

            //Assert
            Assert.IsType<AmountCampaign>(coupon);
        }

        [Fact]
        public void GetDiscount_WhenCalledForAmountCampaign_ReturnsDiscount()
        {
            //Arrange
            double discountAmount = 10;
            double totalPrice = 200;
            double expectedDiscount = 10;
            Category category = new Category("Food");
            CampaignFactory factory = new CampaignFactory();
            AmountCampaign campaign = factory.ProduceCampaign(category, 20, 3, DiscountType.Amount) as AmountCampaign;

            //Act
            double discount = campaign.GetDiscount(totalPrice);

            //Assert
            Assert.Equal(expectedDiscount, discountAmount);
        }

        [Fact]
        public void ProduceCampaign_WhenCalledForRateCampaign_ReturnsRateCampaignObject()
        {
            //Arrange
            Category category = new Category("Food");
            CampaignFactory factory = new CampaignFactory();

            //Act
            ICampaign coupon = factory.ProduceCampaign(category, 50, 10, DiscountType.Rate);

            //Assert
            Assert.IsType<RateCampaign>(coupon);
        }

        [Fact]
        public void ProduceCampaign_WhenCalledWithNullCategory_ThrowsException()
        {
            //Arrange
            CampaignFactory factory = new CampaignFactory();

            //Act && Assert
            var exception = Assert.Throws<ArgumentNullException>(() => factory.ProduceCampaign(null, 50, 10, DiscountType.Rate));
            Assert.Equal("Value cannot be null. (Parameter 'category')", exception.Message);
        }

        [Fact]
        public void GetDiscount_WhenCalledForAmountCampaign_ReturnsPercentageOfDiscount()
        {
            //Arrange
            double discountRate = 10;
            double totalPrice = 200;
            double expectedDiscount = 20;
            Category category = new Category("Food");
            CampaignFactory factory = new CampaignFactory();
            RateCampaign campaign = factory.ProduceCampaign(category, discountRate, 3, DiscountType.Rate) as RateCampaign;

            //Act
            double discount = campaign.GetDiscount(totalPrice);

            //Assert
            Assert.Equal(expectedDiscount, discount);
        }
    }
}
