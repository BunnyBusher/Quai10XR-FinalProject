using Checklist.Runtime;
using Foundation.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelManager.Runtime
{
    public class LevelSelector : FBehaviour
    {

        #region Unity API

        private void Awake()
        {
            SetFact("levelSelector",this);
        }

        private void Start()
        {
            _activeScene = SceneManager.GetActiveScene();
            _toggleGroup = GetComponent<ToggleGroup>();
        }

        #endregion

        #region Main Method

        public void OnStartButtonLoadLevel()
        {
            _levelData = null;
            _levelData = _toggleGroup.GetFirstActiveToggle().gameObject.GetComponent<LevelData>();
            if (_levelData is null)
            {
                Debug.LogWarning("no level data on this toggle");
                return;
            }
            SceneManager.LoadSceneAsync(_levelData.m_levelScene,LoadSceneMode.Additive).completed += StartScene;
        }

        #endregion

        #region Main Method

        public void InitialisePlaneDisplay(GameObject plane, ChecklistScriptableObject checklistSO)
        {
            _planeInScene = plane;
            _planeInScene.SetActive(true);
            SetFact("checklistSO",checklistSO);
        }


        public void OnToggleChange(GameObject plane,ChecklistScriptableObject checklistSO)
        {
            _planeInScene.SetActive(false);
            _planeInScene = plane;
            _planeInScene.SetActive(true);
            SetFact("checklistSO",checklistSO);
        }

        #endregion
        
        #region Utils

        private void StartScene(AsyncOperation obj)
        {
            obj.allowSceneActivation = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_levelData.m_levelScene));
            SceneManager.UnloadSceneAsync(_activeScene);
            GetFact<ChecklistDisplayer>("checklistDisplayer").InitialiseCheckBoard();
        }

        

        #endregion
        
        #region Private

        private ToggleGroup _toggleGroup;
        private LevelData _levelData;
        private Scene _activeScene;
        private GameObject _planeInScene;

        #endregion
    }
}
