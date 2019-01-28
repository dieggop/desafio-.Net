using System;

namespace desafio_.Net.Exceptions
{
    public class ExceptionExists : Exception
    {

    public ExceptionExists(){}

    public ExceptionExists(string message) : base(message) {}
    public ExceptionExists(string message, Exception innerException) : base(message){}
    
    }
}