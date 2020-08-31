using Moq;
using ShoppingCart.Business.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShoppingCart.UnitTest
{
    public class CategoryTests
    {
        [Fact]
        public void AddProduct_WhenNewProductCreated_AddThatProductToCategory()
        {
            //Arrange & Act
            Category eletronicCategory = new Category("Electronic");
            Product samsung = new Product("Samsung", 1000, eletronicCategory);

            //Assert
            Assert.Contains(samsung, eletronicCategory.products);
        }

        [Fact]
        public void AddCampaign_WhenNewCampaignCreated_AddThatCampaignToCategory()
        {
            //Arrange & Act
            Category foodCategory = new Category("Food");
            CampaignFactory campaignFactory = new CampaignFactory();
            AmountCampaign campaign = campaignFactory.ProduceCampaign(foodCategory, 10, 2, Business.DiscountType.Amount) as AmountCampaign;

            //Assert
            Assert.Contains(campaign, foodCategory.campaigns);            
        }

        [Fact]
        public void CreateInstance_WhenCreatedWithParent_CateagoryHasParent()
        {
            //Arrange & Act
            Category foodCategory = new Category("Food");
            Category breadCategory = new Category("Bread", foodCategory);

            //Assert
            Assert.Equal(foodCategory, breadCategory.ParentCategory);
        }
    }
}
