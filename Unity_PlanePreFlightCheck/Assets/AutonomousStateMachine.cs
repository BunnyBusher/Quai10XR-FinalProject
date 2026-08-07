using UnityEngine;

public class AutonomousStateMachine : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_closeCheckParameter, true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= .9f) animator.SetBool(_openCheckParameter, false);
    }

    [SerializeField] private string _openCheckParameter;
    [SerializeField] private string _closeCheckParameter;
}
