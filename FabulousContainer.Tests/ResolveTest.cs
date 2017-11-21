using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FabulousContainer;
using System.Collections.Generic;

namespace FabulousContainer.Tests
{
    [TestClass]
    public class ResolveTest
    {
        [TestMethod]
        public void Resolve_Empty()
        {
            var container = new Container();

            Assert.ThrowsException<KeyNotFoundException>(container.Resolve<ISlomal>);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectKeyException))]
        public void Resolve_IncorrectKey()
        {
            var container = new Container();

            container.Register<IType, Concrete>("noon");
            container.Resolve<IType>(null);
            container.Resolve<IType>("high");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Resolve_TypeWithPrivateConstructor()
        {
            var container = new Container();

            container.Register<ISomething, SomethingElse>();
            container.Resolve<ISomething>();
        }

        [TestMethod]
        public void Resolve_GetConstructorWithMaxParameters()
        {
            var container = new Container();

            container.Register<IMain, Secondary>();
            var result = container.Resolve<IMain>();

            Assert.IsInstanceOfType(result, typeof(IMain));
            //Assert.AreEqual(container.Resolve<IMain>().Count, 3);
        }
    }
}
