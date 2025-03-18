using System;
using System.Collections.Generic;

namespace Utils
{
    public static class ServicesLocator
    {
        private static Dictionary<Type, object> registeredServices = new();

        public static void Register<T>(object service)
        {
            registeredServices.TryAdd(typeof(T), service);
        }

        public static void Unregister<T>()
        {
            registeredServices.Remove(typeof(T));
        }

        public static T Get<T>()
        {
            return (T) registeredServices[typeof(T)];
        }
    }
}