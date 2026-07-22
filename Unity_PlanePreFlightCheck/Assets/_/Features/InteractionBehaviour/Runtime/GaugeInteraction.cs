using PrimeTween;
using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class GaugeInteraction : MonoBehaviour
    {
        #region Main Method

        public void TurnGaugeByBool(bool value)
        {
            if (_isTweenInProgress) return;
            _isTweenInProgress = true;

            
            float targetYAngle = Mathf.Lerp(_minimumYAngle, _maximumYAngle, value ? _valueOnActivation : 0f);
            Quaternion targetRotation = Quaternion.Euler(0f, targetYAngle, 0f);
            
            Sequence.Create()
                .Chain(Tween.LocalRotation(_needleAnchor, targetRotation, _duration, _ease)).OnComplete(() => 
                    _isTweenInProgress = false);

        }

        #endregion
        
        
        #region Private
        
        
        [Header("Gauge properties")]
        [SerializeField] private float _minimumYAngle;
        [SerializeField] private float _maximumYAngle;
        [Range(0f,1f),SerializeField] private float _valueOnActivation;
        [SerializeField] private Transform _needleAnchor;

        [Header("Tween properties")] 
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _ease = Ease.OutElastic;
        private bool _isTweenInProgress;

        #endregion
    }
}
