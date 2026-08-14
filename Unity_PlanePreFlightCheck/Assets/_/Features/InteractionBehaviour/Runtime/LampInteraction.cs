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
            _materialPropertyBlock.SetInt("_Emissive_ON_OFF",value ? 1 : 0);
            _materialPropertyBlock.SetColor("_HDR", value ? _color : Color.white);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }
        

        #endregion

        #region Private and Protected

        [SerializeField] private Color _color;
        private Renderer _renderer;
        private MaterialPropertyBlock _materialPropertyBlock;

        #endregion
    }
}
