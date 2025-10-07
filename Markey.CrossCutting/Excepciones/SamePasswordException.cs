using System;
namespace Markey.CrossCutting.Excepciones;

public class SamePasswordException : Exception
{
    public string Error { get; set; } = "La carpeta no fue encontrada";

}
