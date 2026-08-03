using UnityEngine;

namespace InteractionBehaviour.Runtime
{
    public class HightlightInteraction : MonoBehaviour
    {
        #region Unity APi

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }


        private void Start()
        {
            _meshRenderer.enabled = false;
            _startingColor =  _meshRenderer.material.GetColor("_rimColor");
            _propertyBlock = new MaterialPropertyBlock();
        }


        private void Update()
        {
            if (_isHovered && _interpolationTime < 1f)
            {
                _interpolationTime += Time.deltaTime * _speedMultiplier ;
            }
            else if (!_isHovered && _interpolationTime > 0f)
            {
                _interpolationTime -= Time.deltaTime * _speedMultiplier;
            }
            
            _meshRenderer.enabled = _interpolationTime > 0f;
            
            _propertyBlock.SetColor("_rimColor",Color.Lerp(_startingColor,Color.gold,_interpolationTime));
            _meshRenderer.SetPropertyBlock(_propertyBlock);
        }

        #endregion


        #region Main Methods

        public void OnHoverEnter()
        {
            _isHovered = true;
        }

        public void OnHoverExit()
        {
            _isHovered = false;
        }

        #endregion
        
        #region Private
        
        [SerializeField, Range(0.0001f,10f)]private float _speedMultiplier = 2f;

        private MeshRenderer _meshRenderer;
        private bool _isHovered = false;
        private MaterialPropertyBlock _propertyBlock;
        private float _interpolationTime;
        private Color _startingColor;


        #endregion
    }
}
