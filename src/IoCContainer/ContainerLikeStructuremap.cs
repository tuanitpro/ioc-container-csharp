using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCContainer
{
    public class SimpleContainer
    {
        private readonly Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public ContainerBuilder For<TSource>()
        {
            return For(typeof(TSource));
        }

        public ContainerBuilder For(Type sourceType)
        {
            return new ContainerBuilder(this, sourceType);
        }

        public TSource Resolve<TSource>()
        {
            return (TSource)Resolve(typeof(TSource));
        }

        public object Resolve(Type sourceType)
        {
            if (_map.ContainsKey(sourceType))
            {
                var desctinationType = _map[sourceType];
                return CreateInstance(desctinationType);
            }
            else if (sourceType.IsGenericType && _map.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                var destination = _map[sourceType.GetGenericTypeDefinition()];
                var closedDesctination = destination.MakeGenericType(sourceType.GetGenericArguments());
                return CreateInstance(closedDesctination);
            }
            else if (!sourceType.IsAbstract)
            {
                return CreateInstance(sourceType);
            }
            else
            {
                throw new InvalidOperationException("Could not resolve " + sourceType.FullName);
            }
        }

        private object CreateInstance(Type desctinationType)
        {
            var paramters = desctinationType.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Count())
                .First()
                .GetParameters()
                .Select(p => Resolve(p.ParameterType)).ToArray();

            return Activator.CreateInstance(desctinationType, paramters);
        }

        public class ContainerBuilder
        {
            private readonly Container _container;
            private readonly Type _sourceType;

            public ContainerBuilder(Container container, Type sourceType)
            {
                _container = container;
                _sourceType = sourceType;
            }

            public ContainerBuilder Use<TDesctination>()

            {
                return Use(typeof(TDesctination));
            }

            public ContainerBuilder Use(Type destinationType)
            {
                _container._map.Add(_sourceType, destinationType);
                return this;
            }
        }
    }
}
