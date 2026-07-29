using Foundation.Runtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

namespace CharacterController.Runtime
{
    public class PlayerRegister : FBehaviour
    {
        private void Start()
        {
            _locomotionMediator = GetComponent<LocomotionMediator>();
            if (_locomotionMediator is null)
            {
                Debug.LogWarning("Character Controller not found on " + gameObject.name,gameObject);
                return;
            }
            SetFact("playerMovement",_movementObject);
            SetFact("playerTeleport",_teleportObject);
            SetFact("playerTransform",_locomotionMediator.xrOrigin.transform);
        }

        #region Private

        private LocomotionMediator _locomotionMediator;
        [SerializeField] private GameObject _movementObject;
        [SerializeField] private GameObject _teleportObject;

        #endregion
    }
}
