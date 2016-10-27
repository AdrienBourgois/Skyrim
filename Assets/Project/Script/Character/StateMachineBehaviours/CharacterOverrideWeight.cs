using UnityEngine;

public class CharacterOverrideWeight : ACharacterAnimatorBehaviour
{
    public override void OnStateEnter(Animator _animator, AnimatorStateInfo _stateInfo, int _layerIndex)
    {
        base.OnStateEnter(_animator, _stateInfo, _layerIndex);
        _animator.SetLayerWeight(_layerIndex, 1.0f);
    }

    public override void OnStateExit(Animator _animator, AnimatorStateInfo _stateInfo, int _layerIndex)
    {
        base.OnStateExit(_animator, _stateInfo, _layerIndex);
        _animator.SetLayerWeight(_layerIndex, 0.0f);
    }
}
