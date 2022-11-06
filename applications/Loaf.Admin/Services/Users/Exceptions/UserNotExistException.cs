namespace Loaf.Admin.Services.Users.Exceptions
{
    public class UserNotExistException : Exception
    {
        public UserNotExistException(string message) : base(message)
        {

        }
    }
}
