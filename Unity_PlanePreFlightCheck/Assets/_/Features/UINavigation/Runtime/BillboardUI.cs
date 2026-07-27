using System;
using Foundation.Runtime;
using UnityEngine;

namespace UINavigation.Runtime
{
    public class BillboardUI : FBehaviour
    {
        #region Unity API

        private void Awake()
        {
            _billboardTransform = transform;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            Vector3 direction =  _billboardTransform.position - _mainCamera.transform.position;
            direction = Vector3.ProjectOnPlane(direction, Vector3.up).normalized;
            _billboardTransform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        #endregion
        
        #region Private

        private Camera _mainCamera;
        private Transform _billboardTransform;

        #endregion
    }
}
