using System;

namespace FabulousContainer
{
    /// <summary>
    /// Throws an exception if current key is empty or doesn't exist.
    /// </summary>
    public class IncorrectKeyException : Exception
    {
        public IncorrectKeyException()
        {
        }

        public IncorrectKeyException(string key) 
            : base(String.Format("The key {0} is incorrect.", key))
        {
        }
    }
}
