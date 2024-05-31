
namespace Kitchen_MVC.Commons.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException() 
        {
            
        }
        public AccessDeniedException(string message) : base(message) 
        {
        }
        public AccessDeniedException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
