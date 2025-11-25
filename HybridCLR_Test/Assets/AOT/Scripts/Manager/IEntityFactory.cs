using UnityEngine;

namespace AOT.Scripts.Manager
{
    public interface IEntityFactory<T>
    {
        T CreateEntity(GameObject targetGo);
        void DestroyEntity(T entity);
    }
}
