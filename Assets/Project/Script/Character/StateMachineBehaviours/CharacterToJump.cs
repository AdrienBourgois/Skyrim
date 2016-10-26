using UnityEngine;

public class CharacterToJump : ACharacterAnimatorBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateMove(stateInfo);
    }
}
