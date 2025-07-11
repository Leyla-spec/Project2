namespace Project2.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public MenuItem MenuItem { get; set; }
        public int MenuItemId { get; set; }
        public int Count { get; set; }
    }
}
