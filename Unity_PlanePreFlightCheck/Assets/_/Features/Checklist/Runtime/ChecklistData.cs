using Foundation.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Checklist.Runtime
{
    public class ChecklistData : FBehaviour
    {
        #region Unity API

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(SendChecklistChange);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(SendChecklistChange);
        }

        #endregion

        #region Utils

        private void SendChecklistChange(bool value)
        {
            if (!HasFact(_checklistName, out bool state))
            {
                Debug.LogError(_checklistName + " : don't exist");
                return;
            }
            
            SetFact(_checklistName, value);
        }

        #endregion

        #region Main Methods

        public void Initialise(string checklistName)
        {
            _checklistName =  checklistName;
            _text.text = _checklistName;
            SetFact(_checklistName,false);
        }

        #endregion
        
        #region Private

        [SerializeField] private TMP_Text _text;
        private string _checklistName;
        [SerializeField] private Toggle _toggle;

        #endregion
    }
}
