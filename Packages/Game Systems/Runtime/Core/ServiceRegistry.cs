using System;
using System.Collections.Generic;

namespace VSLikeGame.Core
{
    public static class ServiceRegistry
    {
        private static readonly Dictionary<Type, object> map = new();

        public static void Register<T>(T instance) where T : class => map[typeof(T)] = instance;
        public static void Unregister<T>() where T : class => map.Remove(typeof(T));

        public static T Get<T>() where T : class
            => map.TryGetValue(typeof(T), out var v) ? (T)v : null;

        public static void Clear() => map.Clear();
    }
}
