using Microsoft.AspNetCore.Identity;

namespace Markey.CrossCutting.Excepciones;

public class RegisterException : Exception
{
    public List<string> ErrorMessages { get; } = new List<string>();

    public RegisterException(string errorMessage)
    {
        ErrorMessages.Add(errorMessage);
    }

    public RegisterException(IEnumerable<IdentityError> errors)
    {
        ErrorMessages = errors.Select(e => e.Description).ToList();
    }
}
