using Foundation.Runtime;
using UnityEngine;

namespace Checklist.Runtime
{
    public class ChecklistDisplayer : FBehaviour
    {
        #region Unity API

        private void Awake()
        {
            if (!_checklistScriptableObject)
            {
                Debug.LogWarning("ChecklistScriptableObject is missing");
                return;
            }
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

            foreach (string name in _checklistScriptableObject.m_name)
            {
                GameObject checklist = Instantiate(_checklistPrefab, _checklistTransform);
                ChecklistData checklistData = checklist.GetComponent<ChecklistData>();
                checklistData.Initialise(name);
            }

            _checklistTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _checklistScriptableObject.m_name.Count * 67f);
        }

        #endregion

        
        #region Private

        [SerializeField] private ChecklistScriptableObject _checklistScriptableObject;
        [SerializeField] private GameObject _checklistPrefab;
        [SerializeField] private RectTransform _checklistTransform;

        #endregion
    }
}
