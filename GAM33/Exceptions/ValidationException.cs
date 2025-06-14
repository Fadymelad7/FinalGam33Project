using Microsoft.AspNetCore.Http.HttpResults;

namespace GAM33.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(IEnumerable<string> errors):base()
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; set; }

    }
}
