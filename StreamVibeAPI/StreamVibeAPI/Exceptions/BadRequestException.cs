using System.CodeDom;

namespace StreamVibeAPI.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) 
        {
            
        }
    }
}
