using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FabulousContainer;


namespace FabulousContainer.Tests
{
    [TestClass]
    public class RegisterTest
    {
        [TestMethod]
        public void Register_AndResolveSuccessful()
        {
            var container = new Container();

            container.Register<IType, Concrete>();
            var instance = container.Resolve<IType>();

            Assert.IsInstanceOfType(instance, typeof(Concrete));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_TryToRegisterSame()
        {
            var container = new Container();

            container.Register<IType, Concrete>();
            container.Register<IType, Concrete>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Register_RegisterWithSameKey()
        {
            var container = new Container();

            container.Register<IType, Concrete>("genji");
            container.Register<ISlomal, OhNo>("genji");
        }

        [TestMethod]
        public void Register_Self()
        {
            var container = new Container();

            container.Register<Dummies, Dummies>();
            var instance = container.Resolve<Dummies>();

            Assert.IsInstanceOfType(instance, typeof(Dummies));
        }

        [TestMethod]
        public void Register_SameInterfaceButDifferentKeys()
        {
            var container = new Container();

            container.Register<IType, Concrete>("first");
            container.Register<IType, NotConcrete>("second");

            var one = container.Resolve<IType>("first");
            var two = container.Resolve<IType>("second");

            Assert.AreNotEqual(one, two);
        }
    }

   

    
}
