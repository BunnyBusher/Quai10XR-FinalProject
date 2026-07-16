using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace CharacterController.Runtime
{
    public class CockpitBehaviour : MonoBehaviour
    {
        #region Unity Api

        private void Awake()
        {
            _dynamicMoveProvider = GetComponent<DynamicMoveProvider>();
        }

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
            _dynamicMoveProvider.enabled = true;
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
            _dynamicMoveProvider.enabled = false;
            _teleportModeExit.action.performed += StartMovementWithJoystick;
        }
        

        #endregion
        
        #region Private and Protected

        private DynamicMoveProvider _dynamicMoveProvider;

        [SerializeField] private InputActionReference _teleportModeExit;
        [SerializeField] private TeleportationProvider _teleportationProvider;
        [SerializeField] private Transform _teleportExitAnchor;


        #endregion
    }
}
