using UnityEngine;

public class CharacterFalling : ACharacterAnimatorBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo _stateInfo, int _layerIndex)
    {
        UpdateMove(_stateInfo);

        RaycastHit hit;
        const float distanceToLand = 3f;
        if (Physics.Raycast(CharacterController.CenterOfMass.position, -Vector3.up, out hit, distanceToLand, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            _animator.SetBool("IsGrounded", true);
        }
    }
}
