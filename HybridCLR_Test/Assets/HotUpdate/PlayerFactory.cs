using UnityEngine;
using AOT.Scripts.Manager;
using AOT.Scripts.Move;

namespace HotUpdate
{
    public class PlayerFactory : IEntityFactory<IPlayerMovement>
    {
        public IPlayerMovement CreateEntity(GameObject targetGo)
        {
            IPlayerMovement playerMovement = targetGo.AddComponent<PlayerMovement>();
            return playerMovement;
        }

        public void DestroyEntity(IPlayerMovement entity)
        {

        }
    }
}
