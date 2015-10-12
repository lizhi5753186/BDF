using System;

namespace Bdf
{
    [Serializable]
    public class BdfException : Exception
    {
        public BdfException():base()
        { }

         public BdfException(string message)
            : base(message)
        {

        }

         public BdfException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
         public BdfException(string format, params object[] args) 
             : base(string.Format(format, args)) 
         { }
    }
}
