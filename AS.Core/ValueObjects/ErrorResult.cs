namespace AS.Core.ValueObjects;

public class ErrorResult : Result
{
    public ErrorResult(string message,string  errorType) : base(false, message)
    {
        ErrorType = errorType;
    }
    public ErrorResult(string message) : base(false, message)
    {
    }

    public ErrorResult() : base(false)
    {
    }

    public string ErrorType { get; set; }
}
