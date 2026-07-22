using Foundation.Runtime;
using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class LampInteraction : FBehaviour
    {
        #region Unity API

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        #endregion
        
        #region Main Methods

        public void TurnLightByBool(bool value)
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            _materialPropertyBlock.SetColor("_BaseColor", value ? Color.red : Color.darkRed);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
            _light.enabled = value;
        }
        

        #endregion

        #region Private and Protected

        [SerializeField] private Light _light;
        private Renderer _renderer;
        private MaterialPropertyBlock _materialPropertyBlock;

        #endregion
    }
}
