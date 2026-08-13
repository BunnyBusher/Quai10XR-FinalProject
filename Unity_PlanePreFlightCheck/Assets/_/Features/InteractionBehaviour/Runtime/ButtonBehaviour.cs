using Foundation.Runtime;
using UnityEngine;
using UnityEngine.Events;

namespace InteractionBehaviour.Runtime
{
    public class ButtonBehaviour : FBehaviour
    {
        #region Public

        public UnityEvent<bool> _onButtonActivation;
        
        #endregion

        #region Unity API

        private void FixedUpdate()
        {
            if (!_sendable) return;

            if (_isActivate && !GetFact<AlternatorAndBatteryBehaviour>("alternatorAndBatteryBehaviour")
                    .PlaneIsPowered())
            {
                _onButtonActivation?.Invoke(false);
                _isSend = false;
            }
            
            if (!_isSend && GetFact<AlternatorAndBatteryBehaviour>("alternatorAndBatteryBehaviour").PlaneIsPowered())
            {
                _onButtonActivation?.Invoke(_isActivate);
                _isSend = true;
            }
        }

        #endregion
       
        
        #region Main Method

        public void ButtonIsPress()
        {
            _isActivate = !_isActivate;
            _meshAnchor.localRotation = _isActivate ? Quaternion.Euler(_rotationOn) : Quaternion.Euler(_rotationOff);
            _isSend = false;
            
            if (!_sendable)_onButtonActivation?.Invoke(_isActivate);

        }

        #endregion

        #region Private

        private bool _isActivate = false;
        [SerializeField] private Transform _meshAnchor;
        [SerializeField] private Vector3 _rotationOn;
        [SerializeField] private Vector3 _rotationOff;

        private bool _isSend;
        [SerializeField]private bool _sendable;

        #endregion
    }
}
