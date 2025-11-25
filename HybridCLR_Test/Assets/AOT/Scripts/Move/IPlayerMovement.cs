using UnityEngine;

namespace AOT.Scripts.Move
{
    public interface IPlayerMovement
    {
        void Move(Vector2 dirInput);
        void TurnAround();
        void Jump();
    }
}
