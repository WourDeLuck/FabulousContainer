using System;

namespace FabulousContainer
{
    /// <summary>
    /// Throws an exception if the current type exists in the Dictionary.
    /// </summary>
    public class TypeAlreadyExistsException : Exception
    {
        public TypeAlreadyExistsException()
        {
        }

        public TypeAlreadyExistsException(Type type)
            : base(String.Format("The type {0} already exists in the Dictionary", type))
        {

        }
    }
}
