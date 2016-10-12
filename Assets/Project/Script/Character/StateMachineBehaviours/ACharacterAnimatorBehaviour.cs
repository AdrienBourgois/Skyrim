using UnityEngine;
using System.Collections;

/// <summary>
/// Character is an Abstract class, used in the Character Animator to change its CapsuleCollider for some animations. Inherites from StateMachineBehaviour.
/// </summary>
public abstract class ACharacterAnimatorBehaviour : StateMachineBehaviour
{
    private ACharacter character = null;
    protected ACharacter Character
    {
        get { return character; }
    }

    private CapsuleCollider capsuleCollider = null;
    protected CapsuleCollider CapsuleCollider
    {
        get { return capsuleCollider; }
    }

    protected class CapsuleColliderCopy
    {
        private Vector3 center;
        public Vector3 Center
        {
            get { return center; }
        }

        private float radius;
        public float Radius
        {
            get { return radius; }
        }

        private float height;
        public float Height
        {
            get { return height; }
        }

        public CapsuleColliderCopy(Vector3 _center, float _radius, float _height)
        {
            center = _center;
            radius = _radius;
            height = _height;
        }
    }
    protected CapsuleColliderCopy originalColliderValues = null;

    [SerializeField]
    protected CapsuleColliderCopy finalColliderValues = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.gameObject.GetComponent<ACharacter>();
        capsuleCollider = character.CapsuleCollider;
        originalColliderValues = new CapsuleColliderCopy(capsuleCollider.center,
                                                         capsuleCollider.radius,
                                                         capsuleCollider.height);
        Debug.Log("ENTERED ANIMATOR");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("UPDATE ANIMATOR");
        capsuleCollider.center = Vector3.Lerp(originalColliderValues.Center, finalColliderValues.Center, stateInfo.normalizedTime);
        Debug.Log("POSITIONS : " + capsuleCollider.center);
        capsuleCollider.radius = Mathf.Lerp(originalColliderValues.Radius, finalColliderValues.Radius, stateInfo.normalizedTime);
        Debug.Log("RADIUS : " + capsuleCollider.radius);
        capsuleCollider.height = Mathf.Lerp(originalColliderValues.Height, finalColliderValues.Height, stateInfo.normalizedTime);
        Debug.Log("HEIGHT : " + capsuleCollider.height);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("EXIT ANIMATOR");
        capsuleCollider.center = finalColliderValues.Center;
        Debug.Log("POSITIONS : " + capsuleCollider.center);
        capsuleCollider.radius = finalColliderValues.Radius;
        Debug.Log("RADIUS : " + capsuleCollider.radius);
        capsuleCollider.height = finalColliderValues.Height;
        Debug.Log("HEIGHT : " + capsuleCollider.height);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
