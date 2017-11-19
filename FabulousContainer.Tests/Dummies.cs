using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabulousContainer.Tests
{
    public class Dummies { }

    public interface IType { }

    public class Concrete : IType { }

    public class NotConcrete : IType { }

    public interface ISlomal { }

    public class OhNo : ISlomal { }

    public interface ISomething { }

    public class Something : ISomething
    {
        public Something (string name, string notName)
        {

        }

        public Something (string name)
        {

        }
    }

    public class SomethingElse : ISomething
    {
        private SomethingElse(string one)
        {

        }
    }
}
