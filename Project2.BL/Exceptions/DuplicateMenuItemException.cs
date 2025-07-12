namespace Project2.BL.Exceptions
{
    public class DuplicateMenuItemException : Exception
    {
        public DuplicateMenuItemException()
        { }
        public DuplicateMenuItemException(string message)
            : base(message)
        { }
    }
}
