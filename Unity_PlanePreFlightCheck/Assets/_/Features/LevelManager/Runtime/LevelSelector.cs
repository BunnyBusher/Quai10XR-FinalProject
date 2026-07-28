using System.Linq;
using Foundation.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelManager.Runtime
{
    public class LevelSelector : FBehaviour
    {

        #region Unity API

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
            _levelData = _toggleGroup.ActiveToggles().First().gameObject.GetComponent<LevelData>();
            if (_levelData is null)
            {
                Debug.LogWarning("no level data on this toggle");
                return;
            }
            Debug.Log("Scene index " + _levelData.m_levelScene);
            SceneManager.LoadSceneAsync(_levelData.m_levelScene,LoadSceneMode.Additive).completed += StartScene;
        }

        #endregion
        
        #region Utils

        private void StartScene(AsyncOperation obj)
        {
            obj.allowSceneActivation = true;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_levelData.m_levelScene));
            SceneManager.UnloadSceneAsync(_activeScene);
        }

        #endregion
        
        #region Private

        private ToggleGroup _toggleGroup;
        private LevelData _levelData;
        private Scene _activeScene;

        #endregion
    }
}
