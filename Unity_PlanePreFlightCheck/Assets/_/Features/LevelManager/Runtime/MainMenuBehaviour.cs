using Foundation.Runtime;
using UnityEngine;

namespace LevelManager.Runtime
{
    public class MainMenuBehaviour : FBehaviour
    {
        #region Unity API

        private void Awake()
        {
            _spawnPosition = transform;
        }

        private void Start()
        {
            GetFact<Transform>("playerTransform").position = _spawnPosition.position;
            SetMovementOnPlayer(false);
        }


        private void OnDisable()
        {
            SetMovementOnPlayer(true);
        }
        

        #endregion

        #region Private

        private Transform _spawnPosition;

        #endregion
    }
}
