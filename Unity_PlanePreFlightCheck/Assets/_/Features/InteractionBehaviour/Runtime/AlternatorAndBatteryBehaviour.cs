using Foundation.Runtime;

namespace InteractionBehaviour.Runtime
{
    public class AlternatorAndBatteryBehaviour : FBehaviour
    {
        #region Unity API

        private void Awake()
        {
            SetFact("alternatorAndBatteryBehaviour", this);
        }

        #endregion

        #region Main Methods

        public void TurnAlternatorOn(bool value) => _isAlternatorOn = value;
        
        public void TurnBatteryOn(bool value) => _isBatteryOn = value;
        
        public bool PlaneIsPowered()
        {
            if (_isAlternatorOn && _isBatteryOn) return true;
            return false;
        }

        #endregion
        
        
        
        #region Private

        private bool _isAlternatorOn;
        private bool _isBatteryOn;

        #endregion
    }
}
