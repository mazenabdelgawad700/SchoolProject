namespace SchoolProject.Infrastructure.Exceptions
{
    public class FailedToAddUserToRoleException : Exception
    {
        public int ErrorCode { get; set; }

        public FailedToAddUserToRoleException() : base() { }

        public FailedToAddUserToRoleException(string message) : base(message) { }

        public FailedToAddUserToRoleException(string message, Exception innerException)
            : base(message, innerException) { }

        public FailedToAddUserToRoleException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
