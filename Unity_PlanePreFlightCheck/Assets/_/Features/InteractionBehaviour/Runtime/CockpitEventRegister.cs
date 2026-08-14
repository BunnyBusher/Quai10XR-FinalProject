using CharacterController.Runtime;
using Foundation.Runtime;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace InteractionBehaviour.Runtime
{
    public class CockpitEventRegister : FBehaviour
    {
        #region Unity API

        private void Awake()
        {
            _teleportationAnchor = GetComponent<TeleportationAnchor>();
        }

        private void OnEnable()
        {
            CockpitBehaviour cockpitBehaviour = GetFact<CockpitBehaviour>("cockpitBehaviour");
            _teleportationAnchor.teleporting.AddListener(cockpitBehaviour.StopMovementWithJoystick);
            cockpitBehaviour.SetTeleportExitAnchor(_exitVisual);
        }

        private void OnDisable()
        {
            _teleportationAnchor.teleporting.RemoveListener(GetFact<CockpitBehaviour>("cockpitBehaviour").StopMovementWithJoystick);
        }

        #endregion

        #region Private


        [SerializeField] private Transform _exitVisual;
        private TeleportationAnchor _teleportationAnchor;

        #endregion
    }
}
