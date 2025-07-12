namespace Project2.BL.Exceptions
{
    public class InvalidPriceRangeException : Exception
    {
        public InvalidPriceRangeException() { }
        public InvalidPriceRangeException(string message)
            : base(message)
        { }
    }
}
