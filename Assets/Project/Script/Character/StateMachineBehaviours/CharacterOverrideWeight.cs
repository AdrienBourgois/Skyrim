using UnityEngine;

public class CharacterOverrideWeight : ACharacterAnimatorBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.SetLayerWeight(layerIndex, 1.0f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetLayerWeight(layerIndex, 0.0f);
    }
}
