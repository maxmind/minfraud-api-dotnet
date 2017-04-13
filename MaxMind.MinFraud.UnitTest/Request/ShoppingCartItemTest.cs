using MaxMind.MinFraud.Request;
using Xunit;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class ShoppingCartItemTest
    {
        [Fact]
        public void TestCategory()
        {
            var item = new ShoppingCartItem(category: "cat1");
            Assert.Equal("cat1", item.Category);
        }

        [Fact]
        public void TestItemId()
        {
            var item = new ShoppingCartItem(itemId: "id5");
            Assert.Equal("id5", item.ItemId);
        }

        [Fact]
        public void TestQuantity()
        {
            var item = new ShoppingCartItem(quantity: 100);
            Assert.Equal(100, item.Quantity);
        }

        [Fact]
        public void TestPrice()
        {
            var item = new ShoppingCartItem(price: 10m);
            Assert.Equal(10m, item.Price);
        }
    }
}