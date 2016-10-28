using System.Collections;
using UnityEngine;

public class EnemyController : ACharacterController
{
    [SerializeField]
    private float distanceMaxDetection = 10.0f;
    [SerializeField]
    private float distanceMaxAttack = 1.5f;

    private Transform target;
    private bool bIsAttacking;

    protected override void Start()
    {
        base.Start();

        // HACK: doesnt work everytime in start.. not normal
        //if (character != null)
        //    characterWeapons.SetCharacter(character);

        StartCoroutine(FindTarget());
    }

    private IEnumerator FindTarget()
    {
        bool searchingPlayer = true;

        while (searchingPlayer)
        {
            Player playerTarget = FindObjectOfType<Player>();
            if (playerTarget != null)
            {
                searchingPlayer = false;
                target = playerTarget.transform;
            }
            else
                yield return new WaitForSeconds(1f);
        }

        StartCoroutine(UpdateAggressivity());
    }

    private IEnumerator UpdateAggressivity()
    {
        bool needUpdate = true;
        int layerMask = (1 << LayerMask.NameToLayer("Weapon"));
        layerMask |= (1 << LayerMask.NameToLayer("Character"));
        layerMask = ~layerMask;

        while (needUpdate)
        {
            RaycastHit hit;
            Vector3 direction = target.position - CenterOfMass.position;
            if (Physics.Raycast(CenterOfMass.position, direction, out hit, distanceMaxDetection, layerMask) && hit.collider.transform.root.transform == target)
            {
                if (!bIsAttacking)
                {
                    ControllerDrawSheathSword();
                    bIsAttacking = true;
                }
            }
            else
            {
                if (bIsAttacking)
                {
                    ControllerDrawSheathSword();
                    bIsAttacking = false;
                }
                ControllerMove(0.0f, 0.0f);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    protected override void Update()
    {
        ResetTriggers();

        if (paused)
        {
            ControllerMove(0f, 0f);
            return;
        }

        if (target != null)
            UpdateIa();
    }

    private void UpdateIa()
    {
        if (bIsAttacking)
        {
            Vector3 direction = target.position - CenterOfMass.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
            if (direction.magnitude < distanceMaxAttack)
            {
                ControllerRightHand();
                ControllerMove(0.0f, 0.2f);
            }
            else
                ControllerMove(0.0f, 1.0f);
        }
    }

    public override void ControllerUse()
    {
        RaycastHit hit;
        const float useMaxDistance = 3f;
        if (Physics.Raycast(target.position, target.forward, out hit, useMaxDistance, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            IUsableObject usableCollider = hit.collider.GetComponent<IUsableObject>();

            if (usableCollider != null)
            {
                usableCollider.OnUse(character);
            }
        }
    }

    public override Transform GetTarget()
    {
        return target;
    }
}