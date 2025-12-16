namespace AS.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Geçersiz iþlemler için istisna sýnýfý
/// </summary>
public class InvalidTransactionException : BaseApplicationException
{
    public InvalidTransactionException(string message) : base(message) { }
}
