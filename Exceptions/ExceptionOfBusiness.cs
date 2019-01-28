using System; 

namespace desafio_.Net.Exceptions
{
    public class ExceptionOfBusiness : Exception
    {
        public ExceptionOfBusiness(){}
        public ExceptionOfBusiness(string message) : base(message){}
        public ExceptionOfBusiness(string message, Exception innerException) : base(message){}
 
    }
}