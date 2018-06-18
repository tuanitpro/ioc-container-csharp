using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCContainer
{
    public class Container
    {
        private readonly Dictionary<Type, Func<object>> _registeredTypes = new Dictionary<Type, Func<object>>();

        public void Register<T>(T obj)
        {
            _registeredTypes.Add(typeof(T), () => obj);
        }

        public void Register<TSource, TDesc>()
        {
            _registeredTypes.Add(typeof(TSource), () => GetInstance(typeof(TDesc)));
        }

        public void Register(Type source, Type desc)
        {
            _registeredTypes.Add(source, () => GetInstance(desc));
        }

        public T GetInstance<T>()
        {
            return (T)GetInstance(typeof(T));
        }

        public object GetInstance(Type type)
        {
            if (_registeredTypes.ContainsKey(type))
            {
                return _registeredTypes[type]();
            }
            var constructor = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .First();

            var args = constructor.GetParameters()
                .Select(param => GetInstance(param.ParameterType)).ToArray();

            return Activator.CreateInstance(type, args);
        }
    }
}