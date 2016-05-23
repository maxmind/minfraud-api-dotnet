using MaxMind.MinFraud.Request;
using NUnit.Framework;

namespace MaxMind.MinFraud.UnitTest.Request
{
    public class ShoppingCartItemTest
    {
        [Test]
        public void TestCategory()
        {
            var item = new ShoppingCartItem(category: "cat1");
            Assert.AreEqual("cat1", item.Category);
        }

        [Test]
        public void TestItemId()
        {
            var item = new ShoppingCartItem(itemId: "id5");
            Assert.AreEqual("id5", item.ItemId);
        }

        [Test]
        public void TestQuantity()
        {
            var item = new ShoppingCartItem(quantity: 100);
            Assert.AreEqual(100, item.Quantity);
        }

        [Test]
        public void TestPrice()
        {
            var item = new ShoppingCartItem(price: 10m);
            Assert.AreEqual(10m, item.Price);
        }
    }
}
