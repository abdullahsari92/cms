using AS.Core.ValueObjects;

namespace AS.Core.Exceptions;



public class ValidationException : BaseApplicationException
{
    private List<ValidationResult> _validationResult;

    public List<ValidationResult> ValidationResult
    {

        get
        {
            var list = _validationResult;

            if (list != null)
            {

                return list;

            }

            return _validationResult = new List<ValidationResult>();

        }

        set => _validationResult = value;

    }

    public ValidationException() : base("Kayýt geçerli deðil")
    {
    }


    public ValidationException(string message) : base(message) { }
}
