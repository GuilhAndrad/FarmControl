namespace FarmControl.Communication.Request;

public class RequestChangePasswordUserJson
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
