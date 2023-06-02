using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectile;

    Transform target;
    bool hasActiveTarget;

    List<Enemy> targetList = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        
        target = null;
        hasActiveTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        CheckIfTargetIsActive();
        CheckIfTargetIsInRange();
        AimWeapon();
    }

    void CheckIfTargetIsActive()
    {
        if (target == null)     // We have no target, ignore
            return;

        if (target.gameObject.activeInHierarchy)    // We have target, and it's active. Ignore
            return;
        
        // We have a target, but it's been made inactive
        RemoveFromTargetList(target.GetComponent<Enemy>());
        LostTarget();
    }

    void CheckIfTargetIsInRange()
    {
        if (target == null)     // We have no target
            return;

        if (targetList.Contains(target.GetComponent<Enemy>()))  // Target is still in the list
            return;

        // Target is no longer in the list
        LostTarget();
    }

    void AimWeapon()
    {
        if (target != null)
        {
            weapon.LookAt(target);
            Attack(true);
        }
        else{
            Attack(false);
        }

        
    }

    void FindTarget()
    {
        if (hasActiveTarget)    // Already have a target. Don't look for any more
            return;

        if (targetList == null || targetList.Count == 0)  // We have no targets to look through
            return;

        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in targetList)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
        hasActiveTarget = true;
    }

    void Attack(bool isActive)
    {
        var emmisionModule = projectile.emission;
        emmisionModule.enabled = isActive;
    }

    void LostTarget()
    {
        target = null;
        hasActiveTarget = false;
    }

    public void AddToTargetList(Enemy toAdd)
    {
        targetList.Add(toAdd);
    }

    public void RemoveFromTargetList(Enemy toRemove)
    {
        targetList.Remove(toRemove);
    }
}
