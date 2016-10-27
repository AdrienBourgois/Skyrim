using UnityEngine;

public class CharacterToJump : ACharacterAnimatorBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator _animator, AnimatorStateInfo _stateInfo, int _layerIndex)
    {
        UpdateMove(_stateInfo);
    }
}
