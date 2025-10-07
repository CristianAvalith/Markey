namespace Markey.CrossCutting.Excepciones;

public class IncorrectCredentialException : Exception
{
    public string Error { get; set; } = "Credenciales incorrectas";
}
