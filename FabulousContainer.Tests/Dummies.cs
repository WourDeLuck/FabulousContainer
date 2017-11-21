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

    public interface IMain
    {
        string Name { get; set; }
        string Put { get; set; }
        int Count { get; set; }
    }

    public class Secondary : IMain
    {
        public string Name { get; set; }
        public string Put { get; set; }
        public int Count { get; set; }

        public Secondary(int count)
        {
            Count = 1;
        }

        public Secondary(string name, int count)
        {
            Name = name;
            Count = 2;
        }

        public Secondary(string name, string put, int count)
        {
            Name = name;
            Put = put;
            Count = 3;
        }
    }
}
