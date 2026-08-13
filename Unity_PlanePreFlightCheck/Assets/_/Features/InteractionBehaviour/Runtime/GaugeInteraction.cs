using Foundation.Runtime;
using PrimeTween;
using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class GaugeInteraction : FBehaviour
    {
        #region Unity API

        private void FixedUpdate()
        {
            if (!_isOnAlternatorAndBattery) return;
            
            if (!_isActivate && GetFact<AlternatorAndBatteryBehaviour>("alternatorAndBatteryBehaviour").PlaneIsPowered())
            {
                TurnGaugeByBool(true);
                _isActivate = true;
            }
            else if (_isActivate && !GetFact<AlternatorAndBatteryBehaviour>("alternatorAndBatteryBehaviour").PlaneIsPowered())
            {
                TurnGaugeByBool(false);
                _isActivate = false;
            }
        }

        #endregion
        
        #region Main Method

        public void TurnGaugeByBool(bool value)
        {
            // if (_isTweenInProgress) return;
            // _isTweenInProgress = true;

            
            float targetYAngle = Mathf.Lerp(_minimumZAngle, _maximumZAngle, value ? _valueOnActivation : 0f);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetYAngle);
            
            Sequence.Create()
                .Chain(Tween.LocalRotation(_needleAnchor, targetRotation, _duration, _ease)).OnComplete(() => 
                    _isTweenInProgress = false);

        }

        #endregion
        
        
        #region Private
        
        
        [Header("Gauge properties")]
        [SerializeField] private float _minimumZAngle;
        [SerializeField] private float _maximumZAngle;
        [Range(0f,1f),SerializeField] private float _valueOnActivation;
        [SerializeField] private Transform _needleAnchor;

        [Header("Tween properties")] 
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _ease = Ease.OutElastic;
        private bool _isTweenInProgress;

        [SerializeField] private bool _isOnAlternatorAndBattery;
        private bool _isActivate;

        #endregion
    }
}
