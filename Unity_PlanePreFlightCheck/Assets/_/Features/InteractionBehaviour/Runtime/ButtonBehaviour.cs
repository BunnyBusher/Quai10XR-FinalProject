using System;
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
       
        
        #region Main Method

        public void SendDebugMessage()
        {
            _isActivate = !_isActivate;
            transform.localRotation = _isActivate ? Quaternion.Euler(0, 0, 20) : Quaternion.Euler(0,0,50);
            _onButtonActivation?.Invoke(_isActivate);
        }

        #endregion

        #region Private

        private bool _isActivate = false;

        #endregion
    }
}
