namespace FarmControl.Communication.Response;
public class ErrorResponseJson
{
    public List<string> Mensages { get; set; }

    public ErrorResponseJson(string mensages)
    {
        Mensages = new List<string>
        {
            mensages
        };
    }

    public ErrorResponseJson(List<string> mensages)
    {
        Mensages = mensages;
    }
}
