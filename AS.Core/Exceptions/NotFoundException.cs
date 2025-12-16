namespace AS.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Bulunamayan kayýtlar için istisna sýnýfý
/// </summary>
public class NotFoundException : BaseApplicationException
{
    public NotFoundException() : base("Kayýt bulunamadý.")
    {
    }

    public NotFoundException(string message) : base(message)
    {

    }

}
