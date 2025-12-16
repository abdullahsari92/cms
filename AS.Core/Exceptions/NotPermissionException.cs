namespace AS.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Bulunamayan kayýtlar için istisna sýnýfý
/// </summary>
public class NotPermissionException : BaseApplicationException
{
    public NotPermissionException() : base("Yetkiniz yok.")
    {
    }


    public NotPermissionException(string message) : base(message)
    {

    }

}
