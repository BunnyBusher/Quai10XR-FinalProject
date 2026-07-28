using System;
using Foundation.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace CharacterController.Runtime
{
    public class CockpitBehaviour : FBehaviour
    {
        #region Unity Api

        private void OnEnable()
        {
            if (_teleportModeExit is null) return;
            _teleportModeExit.action.Enable();
        }

       
       

        private void OnDisable()
        {
            if (_teleportModeExit is null) return;
            _teleportModeExit.action.Disable();
        }

        #endregion

        #region Utils

        private void StartMovementWithJoystick(InputAction.CallbackContext callbackContext)
        {
            SetMovementOnPlayer(true);
            TeleportRequest teleportRequest = new TeleportRequest();
            teleportRequest.destinationPosition = _teleportExitAnchor.position;
            teleportRequest.destinationRotation = _teleportExitAnchor.rotation;
            teleportRequest.matchOrientation = MatchOrientation.WorldSpaceUp;
            _teleportationProvider.QueueTeleportRequest(teleportRequest);
            _teleportModeExit.action.performed -= StartMovementWithJoystick;
        }

        #endregion
        
        
        #region Main Methods

        public void StopMovementWithJoystick()
        {
            SetMovementOnPlayer(false);
            _teleportModeExit.action.performed += StartMovementWithJoystick;
        }
        

        #endregion
        
        #region Private and Protected

        
        [SerializeField] private InputActionReference _teleportModeExit;
        [SerializeField] private TeleportationProvider _teleportationProvider;
        private Transform _teleportExitAnchor;


        #endregion
    }
}
