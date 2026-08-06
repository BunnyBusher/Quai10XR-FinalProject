using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class OilChecker : MonoBehaviour
    {
        #region Main Methods

        public void OnSelectEnter()
        {
            bool isOpenOilAnimationParameter = _animator.GetBool("openOil");
            bool isCloseOilAnimationParameter = _animator.GetBool("closeOil");
            
            
            if (!isOpenOilAnimationParameter && !isCloseOilAnimationParameter)
                _animator.SetBool("openOil", true);
            else if (!isOpenOilAnimationParameter && isCloseOilAnimationParameter)
                _animator.SetBool("closingOil", false);
        }

        
        #endregion

        #region Private

        [SerializeField]private Animator _animator;

        #endregion
    }
}
