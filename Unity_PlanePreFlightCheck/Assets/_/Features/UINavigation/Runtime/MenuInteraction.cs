using Foundation.Runtime;
using UnityEngine;

namespace UINavigation.Runtime
{
    public class MenuInteraction : FBehaviour
    {
        #region Unity API

        private void Awake()=>
            _gameObject = gameObject;
        

        #endregion
        
        #region Main Methods


        public void ShowMenu()=> _gameObject.SetActive(true);
        public void HideMenu()=> _gameObject.SetActive(false);
        public void StartGame(int buildIndex) => ChangeScene(buildIndex);
        public void QuitGame() => Application.Quit();
        

        #endregion

        #region Utils

        

        #endregion

        #region Private And Protected

        [SerializeField]private GameObject _gameObject;

        #endregion
    }
}
