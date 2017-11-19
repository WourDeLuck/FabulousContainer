using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabulousContainer
{
    interface IContainer
    {
        void Register<TResolve, TConcrete>();
        void Register<TResolve, TConcrete>(string key);
        T Resolve<T>();
        T Resolve<T>(string key);
        object Resolve(Type resolve);
    }
}
