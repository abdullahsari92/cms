namespace AS.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Onaylanmamýþ kayýtlar için istisna sýnýfý
/// </summary>
public class NotApprovedException : BaseApplicationException
{
    public NotApprovedException() : base("Kayýt Onaylý deðil")
    {
    }
    public NotApprovedException(string message) : base(message) { }
}
