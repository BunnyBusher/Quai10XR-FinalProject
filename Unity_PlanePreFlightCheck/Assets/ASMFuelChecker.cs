using UnityEngine;

public class ASMFuelChecker : StateMachineBehaviour
{
    #region Unity API

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_checkParameter, false);
        animator.SetBool(_closeCheckParameter, false);
    }

    #endregion
    
    
    #region Private

    [SerializeField] private string _checkParameter;
    [SerializeField] private string _closeCheckParameter;

    #endregion
}
