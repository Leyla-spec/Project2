using Project2.Core.Enumns;

namespace Project2.Core.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Categories Category { get; set; }
    }
}
