using PrimeTween;
using UnityEngine;
using UnityEngine.Events;

namespace InteractionBehaviour.Runtime
{
    public class FillCheckerInteraction : MonoBehaviour
    {
        #region Public


        public UnityEvent m_OnEndAnimation;

        #endregion
        
        #region Unity API

        private void OnEnable()
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            block.SetFloat("_Fill",0f);
            _meshRenderer.SetPropertyBlock(block);
        }

        #endregion
        
        #region Main Methods

        public void OnFill()
        {
            Tween.Custom(0f, 1f, 1f,OnValueChange, Ease.Linear);
        }

        public void OnEnd()
        {
            m_OnEndAnimation?.Invoke();
        }

        

        #endregion

        #region Utils

        private void OnValueChange(float value)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            block.SetFloat("_Fill",value);
            _meshRenderer.SetPropertyBlock(block);
        }

        #endregion

        #region Private

        [SerializeField] private MeshRenderer _meshRenderer;

        #endregion
    }
}
