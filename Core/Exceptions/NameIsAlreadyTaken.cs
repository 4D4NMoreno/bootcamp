namespace Core.Exceptions
{
    public class NameAlreadyTakenException : Exception
    {
        public NameAlreadyTakenException(string message) : base(message)
        {
        }
    }
}