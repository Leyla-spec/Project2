using Project2.Core.Enumns;

namespace Project2.Core.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Categories Categories { get; set; }
    }
}
