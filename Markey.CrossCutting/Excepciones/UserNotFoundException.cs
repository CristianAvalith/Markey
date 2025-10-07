namespace Markey.CrossCutting.Excepciones;

public class UserNotFoundException : Exception
{
    public string Error { get; set; } = "Usuario inexistente";
}
