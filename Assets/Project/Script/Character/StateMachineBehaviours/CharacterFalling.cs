using UnityEngine;

public class CharacterFalling : ACharacterAnimatorBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateMove(stateInfo);

        RaycastHit hit;
        float distanceToLand = 3f;
        if (Physics.Raycast(CharacterController.CenterOfMass.position, -Vector3.up, out hit, distanceToLand, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            animator.SetBool("IsGrounded", true);
        }
    }
}
