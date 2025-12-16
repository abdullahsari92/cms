namespace AS.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Kimlik kullanýcý istisna iþlemlerinde kullanýlacak sýnýf
/// </summary>
public class IdentityUserException : BaseApplicationException
{
    public IdentityUserException(string message) : base(message)
    {

    }
}
