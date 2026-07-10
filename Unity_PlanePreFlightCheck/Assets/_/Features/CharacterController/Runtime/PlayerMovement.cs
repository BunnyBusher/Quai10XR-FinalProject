using Foundation.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using VRInputSystem;

namespace CharacterController.Runtime
{
    public class PlayerMovement : FBehaviour, VRInputSystemAction.IPlayerActions
    {
        #region Unity API

        private void Awake()
        {
            _playerActions =  new VRInputSystemAction();
            _playerActions.Player.SetCallbacks(this);
        }
            
        

        private void OnEnable()=>
            _playerActions.Enable();
        

        private void OnDisable()=>
            _playerActions.Disable();

        #endregion


        #region Input System Actions

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 value = context.ReadValue<Vector2>();
                if (value.magnitude > 1)
                {
                    value.Normalize();
                }
                Debug.Log(value.ToString("f2"));
            }
        }

        #endregion


        #region Private

        private VRInputSystemAction _playerActions;

        #endregion
    }
}
