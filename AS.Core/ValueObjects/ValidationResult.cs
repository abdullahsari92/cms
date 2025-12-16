namespace AS.Core.ValueObjects;

/// <summary>
/// Doðrulama sonucunu 
/// </summary>
public class ValidationResult
{

    public ValidationResult(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Doðrulanamayan özellik
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// Doðrulama hata iletisi
    /// </summary>
    public string ErrorMessage { get; set; }

}
