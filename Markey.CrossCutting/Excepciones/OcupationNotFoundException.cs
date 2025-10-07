namespace Markey.CrossCutting.Excepciones;

public class OcupationNotFoundException : Exception
{
    public string Error { get; set; } = "Ocupación no encontrada";
}
