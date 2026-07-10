using System;
using Foundation.Runtime;
using TMPro;
using UnityEngine;

namespace DisplaySettings.Runtime
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class LanguageInDropDown : FBehaviour
    {
        #region Unity API

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
        }

        private void OnEnable()
        {
            _dropdown.ClearOptions();
            _dropdown.AddOptions(GameManager.Runtime.GameManager.GetAllLanguages());
        }

        #endregion

        #region Private and Protected


        private TMP_Dropdown _dropdown;


        #endregion
    }
}
