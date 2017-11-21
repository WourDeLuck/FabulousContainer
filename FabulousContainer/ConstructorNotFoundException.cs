using System;

namespace FabulousContainer
{
    public class ConstructorNotFoundException : Exception
    {
        public ConstructorNotFoundException()
        { }

        public ConstructorNotFoundException(string message) 
            : base(message)
        { }

        public ConstructorNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    } 
}
