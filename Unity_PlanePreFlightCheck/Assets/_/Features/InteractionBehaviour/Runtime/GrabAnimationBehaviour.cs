using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace InteractionBehaviour.Runtime
{
    public class GrabAnimationBehaviour : MonoBehaviour
    {
        #region Unity Api

        
        private void Start()
        {
            ProgressAnimation(_startAnimationProgress);
            _currentAnimationProgress = _animator.GetFloat(_progressName);
        }

        private void FixedUpdate()
        {
            if (!_isGrabbed) return;
            if (!_grabTransform) return;
            if (!_animator) return;
            
            _currentGrabPosition = _grabTransform.position;
            _currentGrabPosition.x = 0f;
            _currentGrabPosition.z = 0f;
            
            _distance = Vector3.Magnitude(_currentGrabPosition - _startGrabPosition);
            _distanceSign = Mathf.Sign(_currentGrabPosition.y - _startGrabPosition.y);
            

            float progressDistance = _distance * _animationMultiplier * _distanceSign;
            float animationProgress = Mathf.Clamp01(progressDistance + _currentAnimationProgress);
            ProgressAnimation(animationProgress);

        }

        #endregion

        #region Public Method

        public void OnSelectEnter(SelectEnterEventArgs enterEventArgs)
        {
            Debug.Log("EnterDone");
            _isGrabbed = true;
            Vector3 initialPosition = enterEventArgs.interactorObject.transform.position;
            initialPosition.x = 0f;
            initialPosition.z = 0f;
            initialPosition.z = 0f;
            _startGrabPosition = initialPosition;
            _grabTransform = enterEventArgs.interactorObject.transform;
        }

        public void OnSelectExit(SelectExitEventArgs exitEventArgs)
        {
            Debug.Log("ExitDone");
            _isGrabbed = false;
            _grabTransform = null;
        }
        

        #endregion

        #region Utils

        private void ProgressAnimation(float progress)
        {
            _animator.SetFloat(_progressName, progress);
        }

        #endregion
        
        #region Private


        [SerializeField] private float _animationMultiplier = 2f;
        [SerializeField, Range(0f,1f)] private float _startAnimationProgress = .4f;
        private float _currentAnimationProgress;
        
        [SerializeField]private Animator _animator;
        [SerializeField] private string _progressName;
        private bool _isGrabbed;
        private Vector3 _startGrabPosition;
        private Vector3 _currentGrabPosition;
        private Transform _grabTransform;
        private float _distance;
        private float _distanceSign;

        #endregion
    }
}
