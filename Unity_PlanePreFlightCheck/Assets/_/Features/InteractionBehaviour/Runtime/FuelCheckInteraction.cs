using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace InteractionBehaviour.Runtime
{
    public class FuelCheckInteraction : MonoBehaviour
    {
        #region Unity API

        private void Start()
        {
            HideFuelCheck();
        }

        #endregion
        #region Main Methods

        public void OnSelectFuelCheck()
        {
            if (_animator.GetBool("check"))
            {
                _animator.SetBool("check", false);
            }
            else
            {
                _fuelCheck.SetActive(true);
                _animator.SetBool("check", true);
            }
        }

        public void HideFuelCheck()
        {
            _fuelCheck.SetActive(false);
        }

        
        #endregion

        #region Private

        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _fuelCheck;

        #endregion
    }
}
