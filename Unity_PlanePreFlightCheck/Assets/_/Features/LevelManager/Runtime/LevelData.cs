using System;
using Checklist.Runtime;
using Foundation.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace LevelManager.Runtime
{
    [DefaultExecutionOrder(10)]
    public class LevelData : FBehaviour
    {
        #region Public
        

        public int m_levelScene
        {
            get
            {
                return _levelSceneIndex;
            }
        }
        
        public Action<GameObject,ChecklistScriptableObject> m_OnToggleTrue;


        #endregion
        
        #region Unity API

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            _planeInScene.SetActive(false);
            if (_toggle.isOn) _levelSelector.InitialisePlaneDisplay(_planeInScene,_checklistSOForLevel);
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(SendLevelData);
            m_OnToggleTrue += _levelSelector.OnToggleChange;
        }

        private void LateUpdate()
        {
            if (_levelSelector.m_isDoorTweenOn && _toggle.interactable)
                _toggle.interactable = false;
            else if (!_levelSelector.m_isDoorTweenOn && !_toggle.interactable)
                _toggle.interactable = true;
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(SendLevelData);
            m_OnToggleTrue -= _levelSelector.OnToggleChange;
        }

        #endregion

        #region Utils
        
        private void SendLevelData(bool value)
        {
            if (value)
                m_OnToggleTrue?.Invoke(_planeInScene,_checklistSOForLevel);
        }

        #endregion
        
        
        
        #region Private


        [SerializeField] private GameObject _planeInScene;
        [SerializeField] private ChecklistScriptableObject _checklistSOForLevel;
        [SerializeField] private int _levelSceneIndex;
        private Toggle _toggle;
        private LevelSelector _levelSelector => GetFact<LevelSelector>("levelSelector");

        #endregion
    }
}
