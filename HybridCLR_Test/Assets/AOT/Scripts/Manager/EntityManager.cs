using System;
using System.Collections.Generic;
using AOT.Scripts.Core;
using UnityEngine;

namespace AOT.Scripts.Manager
{
    public class EntityManager : SafeSingleton<EntityManager>
    {
        private Dictionary<Type,object> _entities = new();

        public void RegisterFactory<T>(IEntityFactory<T> factory)
        {
            Type t = typeof(T);
            if (_entities.ContainsKey(t))
            {
                _entities[t] = factory;
            }
            else
            {
                _entities.Add(t, factory);
            }
        }

        public T GetEntityFactory<T>(GameObject targetGo)
        {
            Type t = typeof(T);
            if (_entities.TryGetValue(t, out object factoryObj))
            {
                if (factoryObj is IEntityFactory<T> factory)
                {
                    return factory.CreateEntity(targetGo);
                }
            }
            return default;
        }

    }
}
