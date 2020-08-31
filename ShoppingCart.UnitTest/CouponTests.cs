using ShoppingCart.Business;
using ShoppingCart.Business.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ShoppingCart.UnitTest
{
    public class CouponTests
    {
        [Fact]
        public void ProduceCoupon_WhenCalledForAmountCoupon_ReturnsAmountCouponObject()
        {
            //Arrange
            CouponFactory factory = new CouponFactory();

            //Act
            ICoupon coupon = factory.ProduceCoupon(50, 10, DiscountType.Amount);

            //Assert
            Assert.IsType<AmountCoupon>(coupon);
        }

        [Fact]
        public void GetDiscount_WhenCalledForAmountCoupon_ReturnsDiscount()
        {
            //Arrange
            CouponFactory factory = new CouponFactory();
            AmountCoupon coupon = factory.ProduceCoupon(50, 10, DiscountType.Amount) as AmountCoupon;

            //Act
            double discount = coupon.GetDiscount(120);

            //Assert
            Assert.Equal(coupon.Discount, discount);
        }

        [Fact]
        public void ProduceCoupon_WhenCalledForRateCoupon_ReturnsRateCouponObject()
        {
            //Arrange
            CouponFactory factory = new CouponFactory();

            //Act
            ICoupon coupon = factory.ProduceCoupon(50, 10, DiscountType.Rate);

            //Assert
            Assert.IsType<RateCoupon>(coupon);
        }

        [Fact]
        public void GetDiscount_WhenCalledForRateCoupon_ReturnsPercentageOfDiscount()
        {
            //Arrange
            double discountRate = 10;
            double givenAmount = 200;
            double expectedDiscount = 20;
            CouponFactory factory = new CouponFactory();
            RateCoupon coupon = factory.ProduceCoupon(50, discountRate, DiscountType.Rate) as RateCoupon;

            //Act
            double discount = coupon.GetDiscount(givenAmount);

            //Assert
            Assert.Equal(expectedDiscount, discount);
        }
    }
}
