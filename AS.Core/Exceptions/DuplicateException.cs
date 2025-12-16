namespace AS.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Mükerrer kayýtlar için istisna sýnýfý
/// </summary>
public class DuplicateException : BaseApplicationException
{
    public DuplicateException(string message) : base(message)
    {

    }

    public DuplicateException() : base("Bu Kayýt daha önce eklenmiþtir.")
    {
    }



}
