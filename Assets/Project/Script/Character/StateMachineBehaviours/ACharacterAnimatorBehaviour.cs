using UnityEngine;

/// <summary>
/// Character is an Abstract class, used in the Character Animator to change its CapsuleCollider for some animations. Inherites from StateMachineBehaviour.
/// </summary>
public abstract class ACharacterAnimatorBehaviour : StateMachineBehaviour
{
    #region Collider values
    protected static readonly CapsuleColliderCopy colliderStanding   = new CapsuleColliderCopy(new Vector3(  0.0f,  0.9f,  0.0f),  0.3f, 1.9f);
    protected static readonly CapsuleColliderCopy colliderToJump     = new CapsuleColliderCopy(new Vector3(-0.05f, 1.28f, 0.05f), 0.35f, 1.3f);
    protected static readonly CapsuleColliderCopy colliderFalling    = new CapsuleColliderCopy(new Vector3(  0.1f,  1.0f,  0.1f), 0.35f, 1.3f);
    protected static readonly CapsuleColliderCopy colliderCrouching  = new CapsuleColliderCopy(new Vector3( 0.08f,  0.7f, 0.14f), 0.38f, 1.5f);

    protected static readonly CapsuleColliderCopy colliderSwordShield             = new CapsuleColliderCopy(new Vector3( 0.05f, 0.85f, 0.06f),  0.3f, 1.75f);
    protected static readonly CapsuleColliderCopy colliderSwordShieldBlock        = new CapsuleColliderCopy(new Vector3( 0.04f,  0.8f, 0.07f),  0.3f,  1.6f);
    protected static readonly CapsuleColliderCopy colliderSwordShieldAttack       = new CapsuleColliderCopy(new Vector3(-0.11f,  0.7f, 0.55f), 0.38f,  1.5f);
    protected static readonly CapsuleColliderCopy colliderSwordShieldToJump       = new CapsuleColliderCopy(new Vector3( 0.14f, 1.43f, 0.16f),  0.4f,  1.3f);
    protected static readonly CapsuleColliderCopy colliderSwordShieldCrouch       = new CapsuleColliderCopy(new Vector3( 0.05f,  0.6f, 0.02f), 0.38f, 1.25f);
    protected static readonly CapsuleColliderCopy colliderSwordShieldCrouchBlock  = new CapsuleColliderCopy(new Vector3( 0.14f,  0.6f, 0.16f), 0.42f, 1.25f);
    protected static readonly CapsuleColliderCopy colliderSwordShieldCrouchAttack = new CapsuleColliderCopy(new Vector3(-0.24f,  0.6f, 0.68f), 0.38f, 1.25f);
    #endregion

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

    private ACharacterController charController = null;
    protected ACharacterController CharacterController
    {
        get { return charController; }
    }

    private float moveX = 0f;
    private float moveZ = 0f;
    private float lastUpdateTime = 0f;
    
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
        charController = animator.gameObject.GetComponent<ACharacterController>();

        moveX = animator.GetFloat("MoveX");
        moveZ = animator.GetFloat("MoveZ");
        lastUpdateTime = stateInfo.normalizedTime;

        #region CapsuleCollider
        capsuleCollider = charController.CapsuleCollider;
        originalColliderValues = new CapsuleColliderCopy(capsuleCollider.center,
                                                         capsuleCollider.radius,
                                                         capsuleCollider.height);
        finalColliderValues = originalColliderValues;
        #endregion
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateColliderCapsule(stateInfo);
    }
    
    protected void UpdateMove(AnimatorStateInfo stateInfo)
    {
        float deltaTime = stateInfo.normalizedTime - lastUpdateTime;
        lastUpdateTime = stateInfo.normalizedTime;

        Vector3 forwardTimesX = charController.transform.right * moveX;
        Vector3 forwardTimesZ = charController.transform.forward * moveZ;

        Vector3 direction = forwardTimesX + forwardTimesZ;

        charController.transform.position += Vector3.Lerp(Vector3.zero, direction, deltaTime);
    }

    /// <summary>
    /// Update ColliderCapsule using Lerp between originalColliderValues and finalColliderValues with t as the animation normalizedTime.
    /// </summary>
    /// <param name="stateInfo">The updated stateInfo used to get the actual normalizedTime.</param>
    protected void UpdateColliderCapsule(AnimatorStateInfo stateInfo)
    {
        capsuleCollider.center = Vector3.Lerp(originalColliderValues.Center, finalColliderValues.Center, stateInfo.normalizedTime);
        capsuleCollider.radius = Mathf.Lerp(originalColliderValues.Radius, finalColliderValues.Radius, stateInfo.normalizedTime);
        capsuleCollider.height = Mathf.Lerp(originalColliderValues.Height, finalColliderValues.Height, stateInfo.normalizedTime);
    }

    /// <summary>
    /// Update ColliderCapsule using Lerp between originalColliderValues and finalColliderValues with t as a customTime given.
    /// </summary>
    /// <param name="customTime">The custom value used for Lerp.</param>
    protected void UpdateColliderCapsule(float customTime)
    {
        capsuleCollider.center = Vector3.Lerp(originalColliderValues.Center, finalColliderValues.Center, customTime);
        capsuleCollider.radius = Mathf.Lerp(originalColliderValues.Radius, finalColliderValues.Radius, customTime);
        capsuleCollider.height = Mathf.Lerp(originalColliderValues.Height, finalColliderValues.Height, customTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetColliderCapsule();
    }

    private void SetColliderCapsule()
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
