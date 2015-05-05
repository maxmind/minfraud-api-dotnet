using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem(
            string category = null,
            string itemId = null,
            int? quantity = null,
            decimal? price = null
            )
        {
            Category = category;
            ItemId = itemId;
            Quantity = quantity;
            Price = price;
        }

        [JsonProperty("category")]
        public string Category { get; }

        [JsonProperty("item_id")]
        public string ItemId { get; }

        [JsonProperty("quantity")]
        public int? Quantity { get; }

        [JsonProperty("price")]
        public decimal? Price { get; }

        public override string ToString()
        {
            return $"Category: {Category}, ItemId: {ItemId}, Quantity: {Quantity}, Price: {Price}";
        }
    }
}