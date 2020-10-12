namespace ShoppingApi.Domain
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public bool InInventory { get; set; }
    }
}
