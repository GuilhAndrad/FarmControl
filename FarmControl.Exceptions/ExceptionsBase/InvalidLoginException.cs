namespace FarmControl.Exceptions.ExceptionsBase;

public class InvalidLoginException : FarmControlException
{
    public InvalidLoginException() : base(ResourceMensagesError.LOGIN_INVALIDO)
    {

    }
}
