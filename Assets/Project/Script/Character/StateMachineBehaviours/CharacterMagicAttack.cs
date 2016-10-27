using UnityEngine;

public class CharacterMagicAttack : ACharacterAnimatorBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo _stateInfo, int _layerIndex)
    {
        ACharacterController controller = _animator.gameObject.GetComponent<ACharacterController>();

        if (controller == null)
            Debug.LogError("CharacterMagicAttack.OnStateEnter() - couldn't get component of type ACharacterController in animator");
        controller.MagicActivation();
    }
}
