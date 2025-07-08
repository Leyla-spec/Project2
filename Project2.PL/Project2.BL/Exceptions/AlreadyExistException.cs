namespace Project2.BL.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string message) : base(message)
        {
        }
        public AlreadyExistException() : base()
        {
        }
    }
}
