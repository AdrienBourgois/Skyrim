using UnityEngine;

public class CharacterMagicAttack : ACharacterAnimatorBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ACharacterController controller = animator.gameObject.GetComponent<ACharacterController>();

        if (controller == null)
            Debug.LogError("CharacterMagicAttack.OnStateEnter() - couldn't get component of type ACharacterController in animator");
        controller.MagicActivation();
    }
}
