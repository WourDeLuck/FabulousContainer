using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FabulousContainer
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, Type> _registeredObjects = new Dictionary<Type, Type>();
        private readonly Dictionary<string, KeyValuePair<Type, Type>> _registeredObjectsKey = new Dictionary<string, KeyValuePair<Type, Type>>();
        private string _key = null;

        /// <summary>
        /// Registers a pair of types by adding to the list.
        /// </summary>
        /// <typeparam name="TResolve">First type.</typeparam>
        /// <typeparam name="TConcrete">Second type.</typeparam>
        public void Register<TResolve, TConcrete>()
        {
            // Checks if Dictionary contains the current type.
            if (_registeredObjects.ContainsKey(typeof(TResolve)))
            {
                throw new ArgumentException(string.Format("The type {0} is already existing", typeof(TResolve)));
            }

            // Checks if the second type implements the first one (interface or smth).
            if (typeof(TResolve).IsAssignableFrom(typeof(TConcrete)))
            {
                _registeredObjects.Add(typeof(TResolve), typeof(TConcrete));
            }
        }

        /// <summary>
        /// Registers a pair of types by adding to the list with a key.
        /// </summary>
        /// <typeparam name="TResolve"></typeparam>
        /// <typeparam name="TConcrete"></typeparam>
        /// <param name="key"></param>
        public void Register<TResolve, TConcrete>(string key)
        {
            // Checks if the key exists in Dictionary
            if (_registeredObjectsKey.ContainsKey(key))
            {
                throw new ArgumentException(string.Format("The key {0} has already been registered!", key));
            }

            // Checks if the second type implements the first one (interface or smth).
            if (typeof(TResolve).IsAssignableFrom(typeof(TConcrete)))
            {
                _registeredObjectsKey.Add(key, new KeyValuePair<Type, Type>(typeof(TResolve), typeof(TConcrete)));
            }
        }

        /// <summary>
        /// Resolves the type by creating an instance of the type.
        /// </summary>
        /// <typeparam name="T">A type to resolve</typeparam>
        /// <returns>Instance of type.</returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public T Resolve<T>(string key)
        {
            if(key == null || !_registeredObjectsKey.ContainsKey(key))
            {
                throw new ArgumentNullException(string.Format("Current key is not correct."));
            }

            _key = key;
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Gets instance of the specified component.
        /// </summary>
        /// <param name="resolve">A type of the component</param>
        /// <returns>Instance of type.</returns>
        public object Resolve(Type resolve)
        {
            var regObj = _registeredObjects.FirstOrDefault(x => x.Key == resolve);
            if (regObj.ToString() == null)
            {
                throw new KeyNotFoundException(string.Format("The type is not registered."));
            }

            Type instance;

            // Check if key is used
            if (_key == null)
            {
                instance = _registeredObjects[resolve];
            }

            else
            {
                instance = _registeredObjectsKey[_key].Value;
            }

            return ResolveInstance(instance);
        }

        private object ResolveInstance(Type type)
        {
            // Gets a constructor of the type
            var constructor = SelectConstructor(type);
            // Gets the parameters of the constructor
            var parameters = constructor.GetParameters()
                .Select(parameter => Resolve(parameter.ParameterType))
                .ToArray();

            // Creates an instance of the type using parameters
            object instance = Activator.CreateInstance(type, parameters);
            return instance;
        }

        /// <summary>
        /// Selects a constructor with a max amount of parameters
        /// </summary>
        /// <param name="type">Type to get a constructor of.</param>
        /// <returns>Constructor of a type.</returns>
        private ConstructorInfo SelectConstructor(Type type)
        {
            var constructor = type
                .GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .Last();

            if (constructor == null)
            {
                throw new InvalidOperationException("No constructor available for this type.");
            }

            return constructor;
        }

     
        

    }
}
