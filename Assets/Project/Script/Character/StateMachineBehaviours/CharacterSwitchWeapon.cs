using UnityEngine;

public class CharacterSwitchWeapon : ACharacterAnimatorBehaviour
{
    public delegate void DelegateSwitch();
    public event DelegateSwitch OnSwitch;

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnSwitch != null)
            OnSwitch.Invoke();
    }
}
