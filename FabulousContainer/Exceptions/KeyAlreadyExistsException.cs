using System;

namespace FabulousContainer
{
    /// <summary>
    /// Throws an exception in a case when the current key exists in the Dictionary.
    /// </summary>
    [Serializable]
    public class KeyAlreadyExistsException : Exception
    {
        public KeyAlreadyExistsException()
        {
        }

        public KeyAlreadyExistsException(string message) 
            : base(message)
        {
        }

        public KeyAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
