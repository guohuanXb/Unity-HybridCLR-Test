using System;
using AOT.Scripts.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
namespace AOT.Scripts.Move
{
    public class Player : MonoBehaviour
    {
        public IPlayerMovement _playerMovement;
        private Vector2 _inputDir = Vector2.zero;
        void Start()
        {
            _playerMovement = EntityManager.Instance.GetEntityFactory<IPlayerMovement>(gameObject);
        }

        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            if (_playerMovement != null)
            {
                _playerMovement.Move(_inputDir);
            }
            else
            {
                Debug.Log("_playerMovement == null");
            }
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            _inputDir = ctx.ReadValue<Vector2>();
        }

    }
}
