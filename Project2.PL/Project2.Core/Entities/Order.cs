namespace Project2.Core.Entities
{
    public class Order : BaseEntity
    {
        public OrderItem[] OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
