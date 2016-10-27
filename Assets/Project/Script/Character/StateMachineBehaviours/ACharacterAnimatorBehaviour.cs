using UnityEngine;

/// <summary>
/// Character is an Abstract class, used in the Character Animator to change its CapsuleCollider for some animations. Inherites from StateMachineBehaviour.
/// </summary>
public abstract class ACharacterAnimatorBehaviour : StateMachineBehaviour
{
    protected ACharacterController CharacterController { get; private set; }

    private float moveX;
    private float moveZ;
    private float lastUpdateTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator _animator, AnimatorStateInfo _stateInfo, int _layerIndex)
    {
        CharacterController = _animator.gameObject.GetComponent<ACharacterController>();

        moveX = _animator.GetFloat("MoveX");
        moveZ = _animator.GetFloat("MoveZ");
        lastUpdateTime = _stateInfo.normalizedTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}
    
    protected void UpdateMove(AnimatorStateInfo _stateInfo)
    {
        float deltaTime = _stateInfo.normalizedTime - lastUpdateTime;
        lastUpdateTime = _stateInfo.normalizedTime;

        Vector3 forwardTimesX = CharacterController.transform.right * moveX;
        Vector3 forwardTimesZ = CharacterController.transform.forward * moveZ;

        Vector3 direction = forwardTimesX + forwardTimesZ;

        CharacterController.transform.position += Vector3.Lerp(Vector3.zero, direction, deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
