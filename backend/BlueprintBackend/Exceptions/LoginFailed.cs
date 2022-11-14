namespace BlueprintBackend.Exceptions
{
    public class LoginFailed : Exception
    {
        public LoginFailed(string message) : base(message)
        {
        }
    }
}
