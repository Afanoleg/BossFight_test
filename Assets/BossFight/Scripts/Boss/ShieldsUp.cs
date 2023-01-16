using System;
using UnityEngine;

public class ShieldsUp : StateMachineBehaviour
{
    public bool isInvulnerable = false;
    public event Action shieldsUp = () => { };
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shieldsUp.Invoke();
        isInvulnerable = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isInvulnerable = false;
    }

}
