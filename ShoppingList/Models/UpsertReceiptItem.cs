namespace ShoppingList.Models
{
    public class UpsertReceiptItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int Count { get; set; }
        public string Price { get; set; }
        public DateTime BuyTime { get; set; }

    }
}
