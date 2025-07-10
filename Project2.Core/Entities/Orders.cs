namespace Project2.Core.Entities
{
    public class Orders : BaseEntity
    {
        public List<OrderItem> OrderItem { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
