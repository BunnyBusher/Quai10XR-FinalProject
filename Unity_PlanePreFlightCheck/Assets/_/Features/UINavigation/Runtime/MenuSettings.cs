using Foundation.Runtime;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UINavigation.Runtime
{
    public class MenuSettings : FBehaviour
    {

        #region Unity API

        private void Start()
        {
            _settingsRectTransform.gameObject.SetActive(_isActiveAtStart);
            if (!_isActiveAtStart)
            {
                _settingsRectTransform.localPosition = new Vector3(0f,1000f);
            }
        }

        private void OnEnable()
        {
            _inputToShowSettigns.action.Enable();
            _inputToShowSettigns.action.performed += DisplayUI;
            _quitToMainMenu.onClick.AddListener(QuitToMainMenu);
        }

        private void OnDisable()
        {
            _inputToShowSettigns.action.Disable();
            _inputToShowSettigns.action.performed -= DisplayUI;
            _quitToMainMenu.onClick.RemoveListener(QuitToMainMenu);
        }

        #endregion

        #region Utils

        private void DisplayUI(InputAction.CallbackContext ctx)
        {
            if (_tweenInProgress) return;
            bool currentState = _settingsRectTransform.gameObject.activeSelf;

            if (currentState)
            {
                _tweenInProgress = true;
                Sequence.Create()
                    .Chain(Tween.LocalPositionY(_settingsRectTransform, 1000f, _duration, _hideEase)).OnComplete(() =>
                    {
                        _settingsRectTransform.gameObject.SetActive(false);
                        _tweenInProgress = false;
                    });
            }
            else
            {
                _tweenInProgress = true;
                _settingsRectTransform.gameObject.SetActive(true);
                Sequence.Create()
                    .Chain(Tween.LocalPositionY(_settingsRectTransform,0f,_duration,_displayEase)).OnComplete(()=>
                        _tweenInProgress = false);
            }
        }

        private void QuitToMainMenu()
        {
            if (SceneManager.GetActiveScene().buildIndex == 2) return;
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive).completed +=SetMainMenu;
        }

        private void SetMainMenu(AsyncOperation obj)
        {
            obj.allowSceneActivation = true;
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(activeScene.buildIndex);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        }

        #endregion
        
        #region Private

        [Header("Data and Properties")] 
        [SerializeField] private bool _isActiveAtStart;
        [SerializeField] private RectTransform _settingsRectTransform;
        [SerializeField] private Button _quitToMainMenu;
        
        [Header("Animation properties")]
        [SerializeField] private InputActionReference _inputToShowSettigns;
        [SerializeField] private float _duration = 1f;
        private Vector3 _initialScale;
        private bool _tweenInProgress;
        [SerializeField] private Ease _displayEase = Ease.OutBounce;
        [SerializeField] private Ease _hideEase = Ease.InBounce;

        #endregion
    }
}
