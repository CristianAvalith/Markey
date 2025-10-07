namespace Markey.CrossCutting.Excepciones;
public class ParameterException : Exception
{
    public List<string> Errors { get; set; }

    public ParameterException()
    {

    }
    public ParameterException(List<string> errors) : base()
    {
        Errors = errors;
    }
}
