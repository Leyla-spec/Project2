namespace Project2.BL.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException() { }

        public OrderNotFoundException(string? message) : base(message)
        {
        }

        public OrderNotFoundException(string message, string v)
            : base(message) { }
    }
}
