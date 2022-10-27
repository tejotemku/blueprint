namespace BlueprintBackend.Exceptions
{
    public class EntityDuplicateException : Exception
    {
        public EntityDuplicateException(string message) : base(message)
        {
        }
    }
}
