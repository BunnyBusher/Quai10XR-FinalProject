using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class RayAnimationInteraction : MonoBehaviour
    {
        #region Unity API

        private void Awake()
        {
            if (_checkerGameObject == null) return;
            _checkerGameObject.GetComponent<CheckerBehaviour>().GetParameterName(_openCheckParameter, _closeCheckParameter);
        }

        private void Start()
        {
            if (_checkerGameObject == null) return;
            _checkerGameObject.SetActive(false);
        }

        private  void FixedUpdate()
        {
            if (_animator.GetBool(_openCheckParameter)) return;
            
            if (_isSelected && _progress < 1f)
            {
                _progress += Time.fixedDeltaTime * _speedMultiplier;
                if (_checkerGameObject)
                {
                    if (_progress >= .9f) _checkerGameObject.SetActive(true);
                    else if (_checkerGameObject.activeSelf)
                    {
                        _checkerGameObject.SetActive(false);
                    }
                }
            }
            else if (!_isSelected && _progress > 0f)
            {
                _progress -= Time.fixedDeltaTime * _speedMultiplier;
                
                if (_checkerGameObject && _checkerGameObject.activeSelf)
                {
                    _checkerGameObject.SetActive(false);
                }
            }
            
            
            _animator.SetFloat(_progressName, _progress);
        }

        #endregion
        
        #region Main Methods

        public  void OnSelecterEnter()
        {
            if (_animator.GetBool(_openCheckParameter) || _animator.GetBool((_closeCheckParameter))) return;
            _isSelected = !_isSelected;
        }
        

        #endregion
        
        #region Private

        [SerializeField] private Animator _animator;
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private string _progressName;
        [SerializeField] private GameObject _checkerGameObject;
        [SerializeField] private string _openCheckParameter;
        [SerializeField] private string _closeCheckParameter;
        private bool _isSelected;
        private float _progress;
        

        #endregion
    }
}
