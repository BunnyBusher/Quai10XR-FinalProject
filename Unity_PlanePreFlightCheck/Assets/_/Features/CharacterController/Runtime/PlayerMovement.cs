using Foundation.Runtime;
using Unity.XR.CoreUtils;
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
            _characterController = GetComponent<UnityEngine.CharacterController>();
            _xROrigin = GetComponent<XROrigin>();
            _xROriginHead = _xROrigin.Camera.transform;
        }
            
        

        private void OnEnable()=>
            _playerActions.Enable();


        private void Update()
        {
            Movement();
        }

        


        private void OnDisable()=>
            _playerActions.Disable();

        #endregion


        #region Input System Actions

        public void OnMove(InputAction.CallbackContext context)=>
            _playerInputMovement = context.ReadValue<Vector2>();

        public void OnTurnHead(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                MovementHead(context.ReadValue<Vector2>());
            }
        }
        

        #endregion

        #region Utils

        private void Movement()
        {
            Vector3 direction = _xROriginHead.forward * _playerInputMovement.y + _xROriginHead.right * _playerInputMovement.x;
            direction.y = 0;
            
            _characterController.Move(direction * (_moveSpeed * Time.deltaTime));
        }
        
        private void MovementHead(Vector2 playerInputRotation)
        {
            Vector3 xrRotation = _xROrigin.transform.eulerAngles;
            if (playerInputRotation.x >= 0f)
                _xROrigin.transform.rotation = Quaternion.Euler(xrRotation.x,xrRotation.y + _turnAngle, xrRotation.z);
            else if (playerInputRotation.x <= -0f)
                _xROrigin.transform.rotation = Quaternion.Euler(xrRotation.x,xrRotation.y - _turnAngle, xrRotation.z);
        }

        #endregion


        #region Private
        
        //dev reference
        private VRInputSystemAction _playerActions;
        private UnityEngine.CharacterController _characterController;
        private XROrigin _xROrigin;
        private Transform _xROriginHead;
        
        [Header("Movement parameters")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _turnAngle = 33f;
        
        // Movement Variable
        private Vector2 _playerInputMovement;
        
        #endregion
    }
}
