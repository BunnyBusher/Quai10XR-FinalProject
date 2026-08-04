using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class OilChecker : MonoBehaviour
    {
        #region Main Methods

        public void OnSelectEnter()
        {
            if (!_animator.GetBool("openOil") && !_animator.GetBool("closingOil"))
                _animator.SetBool("openOil", true);
            else if (!_animator.GetBool("openOil") && _animator.GetBool("closingOil"))
                _animator.SetBool("closingOil", false);
        }

        #endregion

        #region Private

        [SerializeField]private Animator _animator;

        #endregion
    }
}
