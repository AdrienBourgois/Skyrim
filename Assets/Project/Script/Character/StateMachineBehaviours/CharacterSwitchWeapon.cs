using UnityEngine;

public class CharacterSwitchWeapon : ACharacterAnimatorBehaviour
{
    public delegate void DelegateSwitch();
    public event DelegateSwitch OnSwitch;

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator _animator, AnimatorStateInfo _stateInfo, int _layerIndex)
    {
        if (OnSwitch != null)
            OnSwitch.Invoke();
    }
}
