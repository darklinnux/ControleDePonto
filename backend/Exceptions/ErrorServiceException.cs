namespace backend.Exceptions
{
    public class ErrorServiceException :Exception
    {
        public ErrorServiceException(string message) : base(message)
        {
        }

        public ErrorServiceException(string nessage, Exception innerException) : base(nessage, innerException)
        {
        }
    }
}
