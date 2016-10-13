using UnityEngine;
using System.Collections;

/// <summary>
/// Character is an Abstract class, used in the Character Animator to change its CapsuleCollider for some animations. Inherites from StateMachineBehaviour.
/// </summary>
public abstract class ACharacterAnimatorBehaviour : StateMachineBehaviour
{
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

    private ACharacter character = null;
    protected ACharacter Character
    {
        get { return character; }
    }

    private float moveX = 0f;
    private float moveZ = 0f;
    private float lastUpdateTime = 0f;

    #region Capsule Trigger
    private CapsuleCollider capsuleTrigger = null;
    protected CapsuleCollider CapsuleTrigger
    {
        get { return capsuleTrigger; }
    }

    protected CapsuleColliderCopy originalTriggerValues = null;
    protected CapsuleColliderCopy finalTriggerValues = null;
    #endregion

    #region Capsule Collider
    private CapsuleCollider capsuleCollider = null;
    protected CapsuleCollider CapsuleCollider
    {
        get { return capsuleCollider; }
    }

    protected CapsuleColliderCopy originalColliderValues = null;
    protected CapsuleColliderCopy finalColliderValues = null;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character = animator.gameObject.GetComponent<ACharacter>();

        moveX = animator.GetFloat("MoveX");
        moveZ = animator.GetFloat("MoveZ");
        lastUpdateTime = stateInfo.normalizedTime;

        #region CapsuleTrigger
        capsuleTrigger = character.CapsuleTrigger;
        originalTriggerValues = new CapsuleColliderCopy(capsuleTrigger.center,
                                                         capsuleTrigger.radius,
                                                         capsuleTrigger.height);
        finalTriggerValues = originalTriggerValues;
        #endregion

        #region CapsuleCollider
        capsuleCollider = character.CapsuleCollider;
        originalColliderValues = new CapsuleColliderCopy(capsuleCollider.center,
                                                         capsuleCollider.radius,
                                                         capsuleCollider.height);
        finalColliderValues = originalColliderValues;
        #endregion
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateTriggerCapsule(stateInfo);
        UpdateColliderCapsule(stateInfo);
    }
    
    protected void UpdateMove(AnimatorStateInfo stateInfo)
    {
        float deltaTime = stateInfo.normalizedTime - lastUpdateTime;
        lastUpdateTime = stateInfo.normalizedTime;

        Vector3 forwardTimesX = character.transform.right * moveX;
        Vector3 forwardTimesZ = character.transform.forward * moveZ;

        Vector3 direction = forwardTimesX + forwardTimesZ;

        Debug.Log("deltaTime == " + deltaTime + " || moveX == " + moveX + " || moveZ == " + moveZ);

        character.transform.position += Vector3.Lerp(Vector3.zero, direction, deltaTime);
    }

    protected void UpdateTriggerCapsule(AnimatorStateInfo stateInfo)
    {
        capsuleTrigger.center = Vector3.Lerp(originalTriggerValues.Center, finalTriggerValues.Center, stateInfo.normalizedTime);
        capsuleTrigger.radius = Mathf.Lerp(originalTriggerValues.Radius, finalTriggerValues.Radius, stateInfo.normalizedTime);
        capsuleTrigger.height = Mathf.Lerp(originalTriggerValues.Height, finalTriggerValues.Height, stateInfo.normalizedTime);
    }

    protected void UpdateColliderCapsule(AnimatorStateInfo stateInfo)
    {
        capsuleCollider.center = Vector3.Lerp(originalColliderValues.Center, finalColliderValues.Center, stateInfo.normalizedTime);
        capsuleCollider.radius = Mathf.Lerp(originalColliderValues.Radius, finalColliderValues.Radius, stateInfo.normalizedTime);
        capsuleCollider.height = Mathf.Lerp(originalColliderValues.Height, finalColliderValues.Height, stateInfo.normalizedTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetTriggerCapsule();
        SetColliderCapsule();
    }

    protected void SetTriggerCapsule()
    {
        capsuleTrigger.center = finalTriggerValues.Center;
        capsuleTrigger.radius = finalTriggerValues.Radius;
        capsuleTrigger.height = finalTriggerValues.Height;
    }

    protected void SetColliderCapsule()
    {
        capsuleCollider.center = finalColliderValues.Center;
        capsuleCollider.radius = finalColliderValues.Radius;
        capsuleCollider.height = finalColliderValues.Height;
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
