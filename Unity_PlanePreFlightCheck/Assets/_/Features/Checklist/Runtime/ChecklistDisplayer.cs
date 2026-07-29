using Foundation.Runtime;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Checklist.Runtime
{
    public class ChecklistDisplayer : FBehaviour
    {
        #region Unity API

        private void Awake()
        {
            if (!_checklistPrefab)
            {
                Debug.LogWarning("ChecklistPrefab is missing");
                return;
            }

            if (!_checklistTransform)
            {
                Debug.LogWarning("ChecklistTransform is missing");
                return;
            }
            
            SetFact("checklistDisplayer",this);
        }


        private void Start()
        {
            _checkboardWithModel.gameObject.SetActive(_isActiveAtStart);
        }


        private void OnDestroy()
        {
            if (_showCheckListAction is null) return;
            _showCheckListAction.action.Disable();
            _showCheckListAction.action.performed -= DisplayUI;
        }

        #endregion

        #region Utils

        private void DisplayUI(InputAction.CallbackContext obj)
        {
            if (_tweenInProgress) return;
            bool currentState = _checkboardWithModel.gameObject.activeSelf;

            if (currentState)
            {
                _tweenInProgress = true;
                Sequence.Create()
                    .Chain(Tween.Scale(_checkboardWithModel, 0, _duration, _hideEase)).OnComplete(()=>
                {
                    _checkboardWithModel.gameObject.SetActive(false);
                    _tweenInProgress = false;
                });
            }
            else
            {
                _tweenInProgress = true;
                _checkboardWithModel.gameObject.SetActive(true);
                Sequence.Create()
                    .Chain(Tween.Scale(_checkboardWithModel, _initialScale, _duration, _displayEase)).OnComplete(()=>
                        _tweenInProgress = false);
            }
        }
        
        
        public void InitialiseCheckBoard()
        {
            ChecklistScriptableObject checklistScriptableObject = GetFact<ChecklistScriptableObject>("checklistSO");
            
            foreach (string name in checklistScriptableObject.m_name)
            {
                GameObject checklist = Instantiate(_checklistPrefab, _checklistTransform);
                ChecklistData checklistData = checklist.GetComponent<ChecklistData>();
                checklistData.Initialise(name);
            }

            _checklistTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, checklistScriptableObject.m_name.Count * 67f);
            _initialScale = _checkboardWithModel.localScale;
            
            if (!_isActiveAtStart) _checkboardWithModel.localScale = Vector3.zero;
            if (_showCheckListAction is null) return;
            
            _showCheckListAction.action.Enable();
            _showCheckListAction.action.performed += DisplayUI;
        }

        #endregion

        
        #region Private

        [Header("Data and Reference")]
        [SerializeField] private bool _isActiveAtStart = false;
        [SerializeField] private GameObject _checklistPrefab;
        [SerializeField] private RectTransform _checklistTransform;
        [SerializeField] private Transform _checkboardWithModel;
        [Header("Animation properties")]
        [SerializeField] private InputActionReference _showCheckListAction;
        [SerializeField] private float _duration = 0.5f;
        private Vector3 _initialScale;
        private bool _tweenInProgress;
        [SerializeField] private Ease _displayEase = Ease.OutBounce;
        [SerializeField] private Ease _hideEase = Ease.InBounce;

        #endregion
    }
}
