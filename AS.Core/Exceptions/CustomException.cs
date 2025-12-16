namespace AS.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Onaylanmamýþ kayýtlar için istisna sýnýfý
/// </summary>
public class CustomException : BaseApplicationException
{
    public CustomException() : base("Özel hata oluþtu.")
    {
    }
    public CustomException(string message) : base(message) { }
}
