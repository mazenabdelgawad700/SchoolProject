namespace SchoolProject.Infrastructure.Exceptions
{
    public class FailedToAddDefaultRoleException : Exception
    {
        public int ErrorCode { get; set; }

        public FailedToAddDefaultRoleException() : base() { }

        public FailedToAddDefaultRoleException(string message) : base(message) { }

        public FailedToAddDefaultRoleException(string message, Exception innerException)
            : base(message, innerException) { }

        public FailedToAddDefaultRoleException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
