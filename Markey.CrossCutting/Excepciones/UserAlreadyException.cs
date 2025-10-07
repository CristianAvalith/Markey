using System;
namespace Markey.CrossCutting.Excepciones;

public class UserAlreadyException : Exception
{
    public string Error { get; set; } = "Este usuario ya existe";

}
