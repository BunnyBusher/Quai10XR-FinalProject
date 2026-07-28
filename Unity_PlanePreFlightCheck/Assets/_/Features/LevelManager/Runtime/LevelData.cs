using Checklist.Runtime;
using Foundation.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManager.Runtime
{
    public class LevelData : FBehaviour
    {
        #region Publics

        public int m_levelScene
        {
            get
            {
                return _levelSceneIndex;
            }
        }

        public ChecklistScriptableObject m_checklistSOForLevel
        {
            get
            {
                return _checklistSOForLevel;
            }
        }
        


        #endregion
        
        #region Private

        [SerializeField] private ChecklistScriptableObject _checklistSOForLevel;
        [SerializeField] private int _levelSceneIndex;

        #endregion
    }
}
