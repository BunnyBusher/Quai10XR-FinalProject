using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class RayAnimationInteraction : MonoBehaviour
    {
        #region Unity API

        private void Start()
        {
            _oilChecker.SetActive(false);
        }

        private  void FixedUpdate()
        {
            if (_animator.GetBool("openOil")) return;
            
            if (_isSelected && _progress < 1f)
            {
                _progress += Time.fixedDeltaTime * _speedMultiplier;
                if (_progress >= .9f) _oilChecker.SetActive(true);
                else if (_oilChecker.activeSelf)
                {
                    _oilChecker.SetActive(false);
                }
            }
            else if (!_isSelected && _progress > 0f)
            {
                _progress -= Time.fixedDeltaTime * _speedMultiplier;
                if (_oilChecker.activeSelf)
                {
                    _oilChecker.SetActive(false);
                }
            }
            
            
            _animator.SetFloat(_progressName, _progress);
        }

        #endregion
        
        #region Main Methods

        public  void OnSelecterEnter()
        {
            Debug.Log("OnSelecterEnter");
            if (_animator.GetBool("openOil")) return;
            _isSelected = !_isSelected;
        }
        

        #endregion
        
        #region Private

        [SerializeField] private Animator _animator;
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private string _progressName;
        [SerializeField] private GameObject _oilChecker;
        private bool _isSelected;
        private float _progress;
        

        #endregion
    }
}
