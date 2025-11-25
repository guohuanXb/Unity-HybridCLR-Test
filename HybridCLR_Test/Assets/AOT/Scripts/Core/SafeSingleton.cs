using System;
using UnityEngine;

namespace AOT.Scripts.Core
{
    public abstract class SafeSingleton<T> where T : class,new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        public static T Instance => _instance.Value;
    }
}
