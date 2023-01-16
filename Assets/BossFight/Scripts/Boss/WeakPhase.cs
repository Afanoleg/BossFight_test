using UnityEngine;

public class WeakPhase : StateMachineBehaviour
{

    public bool isInvulnerable = false;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isInvulnerable = true;
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isInvulnerable = false;
    }

}
