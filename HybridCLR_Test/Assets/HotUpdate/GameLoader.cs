using UnityEngine;
using AOT.Scripts.Manager;
using AOT.Scripts.Move;

namespace HotUpdate
{
    public static class GameLoader
    {
        public static void Init()
        {
            EntityManager.Instance.RegisterFactory<IPlayerMovement>(new PlayerFactory());
            Debug.Log("Init success");
        }
    }
}
