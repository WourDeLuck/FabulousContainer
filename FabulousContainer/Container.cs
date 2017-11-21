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
                // Custom exception
                throw new TypeAlreadyExistsException(typeof(TResolve));
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
                //Custom exception
                throw new KeyAlreadyExistsException("This key exists in the Dictionary already");
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
            // Exception handling
            T res = default(T);
            try
            {
                res = (T)Resolve(typeof(T));
            }
            catch (IncorrectKeyException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        /// <summary>
        /// Resolves the type by creatring its instance (by a key).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Resolve<T>(string key)
        {
            if(key == null || !_registeredObjectsKey.ContainsKey(key))
            {
                // Custom exception
                throw new IncorrectKeyException(key);
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

        /// <summary>
        /// Gets an instance of the class by constructor and parameters.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
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
                throw new InvalidOperationException("No public constructors where found for this type");
            }

            return constructor;
        }
    }
}
