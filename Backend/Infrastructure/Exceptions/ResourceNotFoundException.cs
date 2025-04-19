namespace Infrastructure.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string resourceName)
            : base($"Nie znaleziono zasobu: {resourceName}")
        {
        }
    }
}
