using Moq;
using ShoppingCart.Business;
using ShoppingCart.Business.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShoppingCart.UnitTest
{
    public class ShoppingCartTests
    {
        [Fact]
        public void AddItem_WhenCalled_AddsTheProduct()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();
            Cart shoppingCart = new Cart(mockCalculator.Object);

            //Act
            shoppingCart.AddItem(banana, 3);

            //Assert
            Assert.Equal(3, shoppingCart.productItems[banana]);
        }

        [Fact]
        public void AddItem_WhenCalled_ChangeTheTotalAmount()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();
            Cart shoppingCart = new Cart(mockCalculator.Object);

            //Act
            shoppingCart.AddItem(banana, 3);

            //Assert
            Assert.Equal(30, shoppingCart.TotalAmount);
        }

        [Fact]
        public void ApplyDiscounts_WhenCartIsWithValidProductCountForCampaigns_AppliesMaxDiscount()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);

            Mock<ICampaign> mockCampaign1 = new Mock<ICampaign>();
            mockCampaign1.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(5);
            mockCampaign1.Setup(mock => mock.ItemCount).Returns(2);
            mockCampaign1.Setup(mock => mock.Category).Returns(category);

            Mock<ICampaign> mockCampaign2 = new Mock<ICampaign>();
            mockCampaign2.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(10);
            mockCampaign2.Setup(mock => mock.ItemCount).Returns(3);
            mockCampaign2.Setup(mock => mock.Category).Returns(category);

            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();

            Cart shoppingCart = new Cart(mockCalculator.Object);
            shoppingCart.AddItem(banana, 5);

            //Act
            shoppingCart.ApplyDiscounts(mockCampaign1.Object, mockCampaign2.Object);

            //Assert
            Assert.Equal(10, shoppingCart.CampaignDiscount);
        }

        [Fact]
        public void ApplyDiscounts_WhenCartIsNotWithValidProductCountForCampaigns_DoesntApplyAnyDiscount()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);

            Mock<ICampaign> mockCampaign1 = new Mock<ICampaign>();
            mockCampaign1.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(5);
            mockCampaign1.Setup(mock => mock.ItemCount).Returns(15);
            mockCampaign1.Setup(mock => mock.Category).Returns(category);

            Mock<ICampaign> mockCampaign2 = new Mock<ICampaign>();
            mockCampaign2.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(10);
            mockCampaign2.Setup(mock => mock.ItemCount).Returns(20);
            mockCampaign2.Setup(mock => mock.Category).Returns(category);

            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();

            Cart shoppingCart = new Cart(mockCalculator.Object);
            shoppingCart.AddItem(banana, 5);

            //Act
            shoppingCart.ApplyDiscounts(mockCampaign1.Object, mockCampaign2.Object);

            //Assert
            Assert.Equal(0, shoppingCart.CampaignDiscount);
        }

        [Fact]
        public void ApplyCoupon_WhenCartIsWithMinPurchaseAmountForCoupon_AppliesCoupon()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();
            Cart shoppingCart = new Cart(mockCalculator.Object);
            shoppingCart.AddItem(banana, 6);

            Mock<ICoupon> mockCoupon = new Mock<ICoupon>();
            mockCoupon.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(25);
            mockCoupon.Setup(mock => mock.MinPurchaseAmount).Returns(30);

            //Act            
            shoppingCart.ApplyCoupon(mockCoupon.Object);

            //Assert
            Assert.Equal(25, shoppingCart.CouponDiscount);
        }

        [Fact]
        public void ApplyCoupon_WhenCartIsNotWithMinPurchaseAmountForCoupon_DoesntApplyCoupon()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();
            Cart shoppingCart = new Cart(mockCalculator.Object);
            shoppingCart.AddItem(banana, 6);

            Mock<ICoupon> mockCoupon = new Mock<ICoupon>();
            mockCoupon.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(25);
            mockCoupon.Setup(mock => mock.MinPurchaseAmount).Returns(100);

            //Act            
            shoppingCart.ApplyCoupon(mockCoupon.Object);

            //Assert
            Assert.Equal(0, shoppingCart.CouponDiscount);
        }

        [Fact]
        public void GetTotalAmountAfterDiscounts_WhenCalled_GetsTotalAmountAfterDiscounts()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();
            Cart shoppingCart = new Cart(mockCalculator.Object);
            shoppingCart.AddItem(banana, 5);

            Mock<ICampaign> mockCampaign = new Mock<ICampaign>();
            mockCampaign.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(5);
            mockCampaign.Setup(mock => mock.ItemCount).Returns(3);
            mockCampaign.Setup(mock => mock.Category).Returns(category);

            Mock<ICoupon> mockCoupon = new Mock<ICoupon>();
            mockCoupon.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(10);
            mockCoupon.Setup(mock => mock.MinPurchaseAmount).Returns(10);

            shoppingCart.ApplyDiscounts(mockCampaign.Object);
            shoppingCart.ApplyCoupon(mockCoupon.Object);

            //Act
            double amountAfterDiscounts = shoppingCart.GetTotalAmountAfterDiscounts();

            //Assert
            Assert.Equal(35, amountAfterDiscounts);
        }

        [Fact]
        public void GetCouponDiscount_WhenCalled_GetsCouponDiscount()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();
            Cart shoppingCart = new Cart(mockCalculator.Object);
            shoppingCart.AddItem(banana, 5);

            Mock<ICoupon> mockCoupon = new Mock<ICoupon>();
            mockCoupon.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(10);
            
            shoppingCart.ApplyCoupon(mockCoupon.Object);

            //Act
            double amountAfterDiscounts = shoppingCart.GetCouponDiscount();

            //Assert
            Assert.Equal(10, amountAfterDiscounts);
        }

        [Fact]
        public void GetCampaignDiscount_WhenCalled_GetsCampaignDiscount()
        {
            //Arrange
            Category category = new Category("Food");
            Product banana = new Product("Banana", 10, category);
            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();
            Cart shoppingCart = new Cart(mockCalculator.Object);
            shoppingCart.AddItem(banana, 5);

            Mock<ICampaign> mockCampaign = new Mock<ICampaign>();
            mockCampaign.Setup(mock => mock.GetDiscount(It.IsAny<double>())).Returns(10);

            shoppingCart.ApplyDiscounts(mockCampaign.Object);

            //Act
            double amountAfterDiscounts = shoppingCart.GetCampaignDiscount();

            //Assert
            Assert.Equal(10, amountAfterDiscounts);
        }

        [Fact]
        public void GetDeliveryCost_WhenCalled_GetsDeliveryCost()
        {
            //Arrange            
            Mock<IDeliveryCostCalculator> mockCalculator = new Mock<IDeliveryCostCalculator>();
            Cart shoppingCart = new Cart(mockCalculator.Object);
            mockCalculator.Setup(mock => mock.CalculateFor(shoppingCart)).Returns(30);            

            //Act
            double deliveryCost = shoppingCart.GetDeliveryCost();          

            //Assert
            Assert.Equal(30, deliveryCost);
        }
    }
}
