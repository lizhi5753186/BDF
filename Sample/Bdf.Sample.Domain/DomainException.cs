using System;

namespace Bdf.Sample.Domain
{
    public class DomainException : Exception
    {
        public DomainException():base()
        { }

         public DomainException(string message)
            : base(message)
        {

        }

         public DomainException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
         public DomainException(string format, params object[] args) 
             : base(string.Format(format, args)) 
         { }
    }
}