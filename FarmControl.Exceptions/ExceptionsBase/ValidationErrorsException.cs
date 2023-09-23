namespace FarmControl.Exceptions.ExceptionsBase;

public class ValidationErrorsException : FarmControlException
{
    public List<string> ErrorMensages { get; set; }
    public ValidationErrorsException(List<string> errorMensages) : base(string.Empty)
    {
        ErrorMensages = errorMensages;
    }
}
