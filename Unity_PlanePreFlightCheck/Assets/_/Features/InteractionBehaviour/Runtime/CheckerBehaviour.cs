using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class CheckerBehaviour : MonoBehaviour
    {
        #region Main Methods

        private void Awake()
        {
            if (_animator == null) this.enabled = false;
        }

        public void OnSelectEnter()
        {
            bool isOpenOilAnimationParameter = _animator.GetBool(_openCheckParameter);
            bool isCloseOilAnimationParameter = _animator.GetBool(_closeCheckParameter);
            
            
            if (!isOpenOilAnimationParameter && !isCloseOilAnimationParameter)
                _animator.SetBool(_openCheckParameter, true);
            else if (!isOpenOilAnimationParameter && isCloseOilAnimationParameter)
            {
                if (_isFuelCheck)
                {
                    _animator.SetBool(_checkParameter, true);
                }
                else 
                    _animator.SetBool(_closeCheckParameter, false);
            }
        }

        public void GetParameterName(string open, string close)
        {
            _openCheckParameter = open;
            _closeCheckParameter = close;
        }
        
        
        #endregion

        #region Private

        [SerializeField]private Animator _animator;
        
        [SerializeField] private string _openCheckParameter;
        [SerializeField] private string _closeCheckParameter;
        [SerializeField] private bool _isFuelCheck;
        [SerializeField] private string _checkParameter;

        #endregion
    }
}
