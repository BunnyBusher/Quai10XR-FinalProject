using Checklist.Runtime;
using Foundation.Runtime;
using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelManager.Runtime
{
    [DefaultExecutionOrder(5)]
    public class LevelSelector : FBehaviour
    {
        #region Public

        public bool m_isDoorTweenOn
        {
            get
            {
                return _isDoorTweenOn;
            }
        }
            
        

        #endregion
        
        
        
        #region Unity API

        private void Awake()
        {
            SetFact("levelSelector",this);
        }

        private void Start()
        {
            _activeScene = SceneManager.GetActiveScene();
            _toggleGroup = GetComponent<ToggleGroup>();
            OpenDoor();
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
        

        public void InitialisePlaneDisplay(GameObject plane, ChecklistScriptableObject checklistSO)
        {
            _planeInScene = plane;
            _planeInScene.SetActive(true);
            SetFact("checklistSO",checklistSO);
        }


        public void OnToggleChange(GameObject plane,ChecklistScriptableObject checklistSO)
        {
            if (_isDoorTweenOn) return;
            
            _isDoorTweenOn = true;
            Sequence.Create()
                .Group(Tween.LocalPositionZ(_rightDoor, 0, _closeDuration, _closeEase))
                .Group(Tween.LocalPositionZ(_leftDoor, 0, _closeDuration, _closeEase)).OnComplete(() =>
                {
                    _planeInScene.SetActive(false);
                    _planeInScene = plane;
                    SetFact("checklistSO", checklistSO);
                    _planeInScene.SetActive(true);
                    Sequence.Create()
                        .Chain(Tween.Delay(_waitDuration)).OnComplete(OpenDoor);
                });
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

        private void OpenDoor()
        {
            _isDoorTweenOn = true;
            Sequence.Create()
                .Group(Tween.LocalPositionZ(_rightDoor, -5f, _openDuration, _openEase))
                .Group(Tween.LocalPositionZ(_leftDoor, 5f, _openDuration, _openEase)).OnComplete(()=> _isDoorTweenOn = false);
        }

        

        #endregion
        
        #region Private


        [Header("Plane Transition Tween")] 
        [SerializeField] private Transform _rightDoor;
        [SerializeField] private Transform _leftDoor;
        [SerializeField] private float _closeDuration = .35f;
        [SerializeField] private Ease _closeEase = Ease.OutBounce;
        [SerializeField] private float _waitDuration = .25f;
        
        [SerializeField] private float _openDuration = .5f;
        [SerializeField] private Ease _openEase = Ease.InBounce;
        
        private bool _isDoorTweenOn = false;
        

        private ToggleGroup _toggleGroup;
        private LevelData _levelData;
        private Scene _activeScene;
        private GameObject _planeInScene;

        #endregion
    }
}
