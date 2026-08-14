using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace InteractionBehaviour.Runtime
{
    public class KeepItemInPlayArea : MonoBehaviour
    {
        #region Unity APi

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(OnSelectEnter);
            _grabInteractable.selectExited.AddListener(OnSelectExit);
        }

        private void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(OnSelectEnter);
            _grabInteractable.selectExited.RemoveListener(OnSelectExit);
        }

        private void FixedUpdate()
        {
            if (_isSelected)
                return;
            
            if (_rigidbody.isKinematic) return;

            if (_timer < 61f)
                _timer += Time.fixedDeltaTime;
            
            
            if (transform.position.y < -2f || transform.position.y > 6f) Respawn();
            if (transform.position.z > 3f || transform.position.z < -15f) Respawn();
            if (transform.position.x < -9f || transform.position.x > 9f) Respawn();
            
            if (_timer < 2f) return;
            
            if (_rigidbody.linearVelocity.magnitude < 0.05f || _timer > 60f) _rigidbody.isKinematic = true;
        }

        #endregion

        #region Utils

        private void Respawn() => transform.position = new Vector3(3.3f,0.1f,-2.4f);
        
        
        #endregion

        #region Main Methods

        public void OnSelectEnter(SelectEnterEventArgs args)
        {
            _isSelected = true;
            _rigidbody.isKinematic = false;
            _controller = args.interactorObject.transform;
        }
        
        public void OnSelectExit(SelectExitEventArgs args)
        {
            _isSelected = false;
            _timer = 0f;
            _rigidbody.isKinematic = false;
            _controller = null;
        }

        #endregion

        #region Private

        private XRGrabInteractable _grabInteractable;
        private Rigidbody _rigidbody;
        private bool _isSelected;
        private float _timer;
        private Transform _controller;

        #endregion

    }
}
