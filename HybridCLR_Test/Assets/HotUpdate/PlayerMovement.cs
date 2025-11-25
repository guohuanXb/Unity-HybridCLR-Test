using System;
using AOT.Scripts.Move;
using UnityEngine;

namespace HotUpdate
{
    public class PlayerMovement : MonoBehaviour,IPlayerMovement
    {
        private Rigidbody _rb;
        [SerializeField]private float moveSpeed = 10f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }


        public void Move(Vector2 dirInput)
        {
            var dirV2 = dirInput.normalized;

            _rb.linearVelocity = new Vector3(dirV2.x * moveSpeed, _rb.linearVelocity.y, dirV2.y * moveSpeed);
        }

        public void Jump()
        {

        }

        public void TurnAround()
        {

        }
    }
}