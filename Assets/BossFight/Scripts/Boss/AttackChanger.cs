using UnityEngine;

public class AttackChanger : StateMachineBehaviour
{


    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("AttackChanger", Random.Range(0, 4));
    }
}
